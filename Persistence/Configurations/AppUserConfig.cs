using Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("appUsers");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("userId")
                .ValueGeneratedNever();
            builder.Property(c => c.EmailAddress).HasColumnName("emailAddress").IsRequired().HasMaxLength(50);
            builder.Property(c => c.Name).HasColumnName("userName").IsRequired().HasMaxLength(50);
            builder.Property(c => c.PasswordHashed).HasColumnName("passwordHashed").IsRequired();
        }
    }
}
