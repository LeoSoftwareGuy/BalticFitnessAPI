using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ExerciseConfig : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id)
                .HasColumnName("ExerciseId")
                .ValueGeneratedNever();

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.MuscleGroupId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();

            builder.HasOne(e => e.MuscleGroup)
                .WithMany(e => e.Exercises)
                .HasForeignKey(e => e.MuscleGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
