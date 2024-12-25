using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ExerciseSetConfig : IEntityTypeConfiguration<ExerciseSet>
    {
        public void Configure(EntityTypeBuilder<ExerciseSet> builder)
        {
            builder.ToTable("exerciseSets");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("exerciseSetId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Reps)
              .HasColumnName("reps")
              .IsRequired();

            builder.Property(e => e.Weight)
              .HasColumnName("weight")
              .IsRequired();

            builder.HasOne(e => e.Training)
                .WithMany(e => e.ExerciseSets)
                .HasForeignKey(e => e.TrainingId)
                .OnDelete(DeleteBehavior.Cascade); //means that if the Training entity is deleted, all related ExerciseSet entities will also be deleted.

            builder.HasOne(e => e.Exercise)
                .WithMany(e => e.ExerciseSets)
                .HasForeignKey(e => e.ExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
