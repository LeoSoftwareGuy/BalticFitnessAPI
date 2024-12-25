using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ExerciseConfig : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("exercises");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id)
                .HasColumnName("exerciseId")
                .ValueGeneratedNever();

            builder.Property(x => x.MuscleGroupId)
                .HasColumnName("muscleGroupId")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .HasColumnName("imageUrl")
                .IsRequired();

            builder.HasOne(e => e.MuscleGroup)
                .WithMany(e => e.Exercises)
                .HasForeignKey(e => e.MuscleGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
