
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.SqlDataBase.AuthorizationDB.Models;

namespace Persistence.Interfaces
{
    public interface IAuthorizationDbContext
    {
        DbSet<AppUser> AppUsers { get; set; }

        public DbSet<RefreshTokens> AppUserTokens { get; set; }

        // Expose the Database property for transaction handling
        DatabaseFacade Database { get; }

        // Ensure SaveChangesAsync is part of the interface
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
