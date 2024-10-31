using System.Security.Claims;

namespace Infrastructure.Services.Interfaces
{
    public interface IJwTokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
    }
}
