
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;
using Persistence.SqlDataBase.AuthorizationDB.Models;

namespace Persistence.SqlDataBase.AuthorizationDB
{
    public class AuthorizationDbContext : DbContext, IAuthorizationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshTokens> AppUserTokens { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {
            
        }
    }
}
