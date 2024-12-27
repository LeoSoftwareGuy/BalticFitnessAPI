using Infrastructure.Models.DTOs;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalticsFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutherizationService _autherizationService;

        public AuthController(IAutherizationService autherizationService)
        {
            _autherizationService = autherizationService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _autherizationService.Register(request);
                if (result.IsSuccess)
                {
                    Response.Cookies.Append("refreshToken", result.OutputTokens.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, // Set to true in production
                        Expires = DateTime.UtcNow.AddDays(2)
                    });

                    return Ok(result.OutputTokens.AccessToken);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }

            return BadRequest();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _autherizationService.Login(request);
                if (result.IsSuccess)
                {
                    Response.Cookies.Append("refreshToken", result.OutputTokens.RefreshToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Path = "/",
                        Expires = DateTime.UtcNow.AddDays(2)
                    });

                    return Ok(result.OutputTokens.AccessToken);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            return BadRequest();
        }


        [HttpPost("bio")]
        [Authorize]
        public async Task<IActionResult> BioUpdate([FromBody] BioRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _autherizationService.UpdateBioOfTheUser(request);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            return BadRequest();
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                // Remove the refresh token from the database
                await _autherizationService.DeleteRefreshToken(refreshToken);
                // Clear the HTTP-only cookie
                Response.Cookies.Delete("refreshToken");
            }

            return NoContent();
        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var oldRefreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(oldRefreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }

            var storedRefreshToken = await _autherizationService.GetStoredRefreshTokenAsync(oldRefreshToken);
            if (storedRefreshToken == null || storedRefreshToken.ExpirationDate < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            var result = await _autherizationService.GenerateNewRefreshToken(storedRefreshToken);

            if (!string.IsNullOrEmpty(result.AccessToken) && !string.IsNullOrEmpty(result.RefreshToken))
            {
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(2)
                });

                return Ok(result.AccessToken);
            }

            return BadRequest();
        }
    }
}
