using Application.Support.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.DTOs;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.SqlDataBase.AuthorizationDB.Models;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class AutherizationService : IAutherizationService
    {
        private readonly IAuthorizationDbContext _authorizationDbContext;
        private readonly IJwTokenService _jwtTokenService;
        private readonly ICurrentUserService _currentUserService;
        public AutherizationService(IAuthorizationDbContext authroizationDbContext,
            IJwTokenService jwTokenService,
            ICurrentUserService currentUserService)
        {
            _authorizationDbContext = authroizationDbContext;
            _jwtTokenService = jwTokenService;
        }

        public async Task<ServiceResponse> Register(RegisterRequest request)
        {
            var user = await _authorizationDbContext.AppUsers
                 .FirstOrDefaultAsync(u => u.EmailAddress.Equals(request.Email));

            if (user == null)
            {
                var appUser = new AppUser
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = request.Email,
                    Name = request.UserName,
                    Surname = request.Surname,
                    Age = request.Age,
                    Gender = request.Gender,
                    Nationality = request.Nationality,
                    PasswordHashed = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                OutputTokens tokens = null;

                using (var transaction = await _authorizationDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        await _authorizationDbContext.AppUsers.AddAsync(appUser);
                        var saveResult = await _authorizationDbContext.SaveChangesAsync();

                        if (saveResult > 0)
                        {
                            tokens = await GenerateTokens(appUser);
                        }

                        // Commit transaction if everything is successful
                        await transaction.CommitAsync();

                        var responce = new ServiceResponse { IsSuccess = true, ErrorMessage = string.Empty, OutputTokens = tokens };
                        return responce;
                    }
                    catch (Exception ex)
                    {
                        // Log the error for debugging
                        // _logger.LogError(ex, "Error occurred during registration");

                        // Rollback if something goes wrong
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            else
            {
                return new ServiceResponse { IsSuccess = false, ErrorMessage = "Such user already exists!", OutputTokens = null };
            }
        }


        public async Task<ServiceResponse> Login(LoginRequest request)
        {
            // Find user by email
            var user = await _authorizationDbContext.AppUsers
                .FirstOrDefaultAsync(u => u.EmailAddress.Equals(request.Email));

            // Check if user exists
            if (user != null)
            {
                // Verify the hashed password
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHashed);

                if (isPasswordValid)
                {
                    var tokens = await GenerateTokens(user);
                    var responce = new ServiceResponse { IsSuccess = true, ErrorMessage = string.Empty, OutputTokens = tokens };
                    return responce;
                }
                else
                {
                    return new ServiceResponse { IsSuccess = false, ErrorMessage = "Passwords do not match!", OutputTokens = null };
                }
            }
            else
            {
                return new ServiceResponse { IsSuccess = false, ErrorMessage = "Such user does not exist!", OutputTokens = null };
            }
        }


        public async Task DeleteRefreshToken(string refreshToken)
        {
            var token = await GetStoredRefreshTokenAsync(refreshToken);
            if (token != null)
            {
                _authorizationDbContext.AppUserTokens.Remove(token);
                await _authorizationDbContext.SaveChangesAsync();
            }
        }


        public async Task<RefreshTokens> GetStoredRefreshTokenAsync(string refreshToken)
        {
            var userToken = await _authorizationDbContext.AppUserTokens
                .Include(s => s.User)
                .FirstOrDefaultAsync(u => u.Token.Equals(refreshToken));

            if (userToken != null)
            {
                var token = userToken.Token;
                if (token == refreshToken)
                    return userToken;
                return null;
            }
            return null;
        }


        public async Task<OutputTokens> GenerateNewRefreshToken(RefreshTokens oldRefreshToken)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, oldRefreshToken.User.Name),
                new Claim(ClaimTypes.Email, oldRefreshToken.User.EmailAddress),
                new Claim(ClaimTypes.NameIdentifier, oldRefreshToken.AppUserId.ToString())
            };

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            var existingToken = await _authorizationDbContext.AppUserTokens
                                                              .FirstOrDefaultAsync(t => t.Id == oldRefreshToken.Id);

            if (existingToken != null)
            {
                existingToken.Token = refreshToken;
                existingToken.ExpirationDate = DateTime.UtcNow.AddDays(2);
            }
            else
            {
                var refreshTokenDb = new RefreshTokens
                {
                    AppUserId = oldRefreshToken.AppUserId,
                    Token = refreshToken,
                    ExpirationDate = DateTime.UtcNow.AddDays(2)
                };
                await _authorizationDbContext.AppUserTokens.AddAsync(refreshTokenDb);
            }

            await _authorizationDbContext.SaveChangesAsync();

            return new OutputTokens { AccessToken = accessToken, RefreshToken = refreshToken };
        }



        private async Task<OutputTokens> GenerateTokens(AppUser user)
        {
            var claims = new[]
                    {
                         new Claim(ClaimTypes.Name, user.Name),
                         new Claim(ClaimTypes.Email, user.EmailAddress),
                         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

            var accessToken = _jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            var refreshTokenDb = new RefreshTokens
            {
                AppUserId = user.Id,
                Token = refreshToken,
                ExpirationDate = DateTime.UtcNow.AddDays(2)
            };

            await _authorizationDbContext.AppUserTokens.AddAsync(refreshTokenDb);
            await _authorizationDbContext.SaveChangesAsync();

            var tokens = new OutputTokens { AccessToken = accessToken, RefreshToken = refreshToken };
            return tokens;
        }

    }
}
