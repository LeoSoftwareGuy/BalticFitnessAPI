using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MuscleGroupConfig : IEntityTypeConfiguration<MuscleGroup>
    {
        public void Configure(EntityTypeBuilder<MuscleGroup> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(m => m.Id)
                .HasColumnName("MuscleGroupId")
                .ValueGeneratedNever();

            builder.Property(m => m.Name).HasMaxLength(30).IsRequired();
            builder.Property(m => m.ImageUrl).IsRequired();
            builder.Property(m => m.Type).HasMaxLength(30).IsRequired();

            builder.HasMany(e => e.Exercises)
                .WithOne(e => e.MuscleGroup)
                .HasForeignKey(e => e.MuscleGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
