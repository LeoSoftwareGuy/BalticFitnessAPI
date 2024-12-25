using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MuscleGroupConfig : IEntityTypeConfiguration<MuscleGroup>
    {
        public void Configure(EntityTypeBuilder<MuscleGroup> builder)
        {
            builder.ToTable("muscles");
            builder.HasKey(e => e.Id);

            builder.Property(m => m.Id)
                .HasColumnName("muscleGroupId")
                .ValueGeneratedNever();

            builder.Property(m => m.Name)
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(m => m.ImageUrl)
                .HasColumnName("imageUrl")
                .IsRequired();

            builder.Property(m => m.Type)
                .HasColumnName("type")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(e => e.Exercises)
                .WithOne(e => e.MuscleGroup)
                .HasForeignKey(e => e.MuscleGroupId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
