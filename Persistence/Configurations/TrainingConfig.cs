using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TrainingConfig : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
             .HasColumnName("TrainingId")
            .ValueGeneratedOnAdd();
      
            builder.Property(e => e.Trained)
             .HasColumnType("timestamp")
             .IsRequired();

            builder.Property(e => e.UserId).IsRequired();
        }
    }
}
