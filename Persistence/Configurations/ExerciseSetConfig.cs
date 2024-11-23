using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ExerciseSetConfig : IEntityTypeConfiguration<ExerciseSet>
    {
        public void Configure(EntityTypeBuilder<ExerciseSet> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Reps).HasColumnName("RepetitionsDuringSet")
              .IsRequired();
            builder.Property(e => e.Weight)
              .IsRequired();
            builder.Property(e => e.Pre)
              .IsRequired();

            builder.HasOne(e => e.Training)
                .WithMany(e => e.ExerciseSets)
                .HasForeignKey(e => e.Training_Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Exercise)
                .WithMany(e => e.ExerciseSets)
                .HasForeignKey(e => e.Exercise_Id)
                .OnDelete(DeleteBehavior.Restrict);  // if exerciseSet is deleted, exercise remains the same
        }
    }
}
