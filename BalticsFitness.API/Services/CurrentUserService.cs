using Application.Support.Interfaces;
using System.Security.Claims;

namespace BalticsFitness.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            IsAuthenticated = UserId != null;
        }

        public string UserId { get; }
        public string UserName { get; }

        public bool IsAuthenticated { get; }
    }
}
