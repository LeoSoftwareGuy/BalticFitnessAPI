using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TrainingConfig : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("trainings");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
             .HasColumnName("trainingId")
            .ValueGeneratedOnAdd();
      
            builder.Property(e => e.Trained)
             .HasColumnName("trained")
             .HasColumnType("timestamp with time zone")
             .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("userId")
                .IsRequired();
        }
    }
}
