using Infrastructure.Models;
using Infrastructure.Models.DTOs;
using Persistence.SqlDataBase.AuthorizationDB.Models;

namespace Infrastructure.Services.Interfaces
{
    public interface IAutherizationService
    {
        Task<ServiceResponse> UpdateBioOfTheUser(BioRequest bio);
        Task<OutputTokens> GenerateNewRefreshToken(RefreshTokens oldRefreshToken); 
        Task<RefreshTokens> GetStoredRefreshTokenAsync(string refreshToken);
        Task DeleteRefreshToken(string refreshToken);
        Task<ServiceResponse> Register(RegisterRequest request);
        Task<ServiceResponse> Login(LoginRequest request);

    }
}
