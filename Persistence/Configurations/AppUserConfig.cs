using Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();
            builder.Property(c => c.EmailAddress).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.PasswordHashed).IsRequired();
        }
    }
}
