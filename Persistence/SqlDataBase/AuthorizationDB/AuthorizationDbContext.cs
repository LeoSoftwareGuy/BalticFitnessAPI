using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Authentication;
using Persistence.Configurations;

namespace Persistence.SqlDataBase.AuthorizationDB
{
    public class AuthorizationDbContext : DbContext, IAuthorizationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshTokens> AppUserTokens { get; set; }
        public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }
        }
    }
}
