using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Nutrition
{
    public class MealConfig : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                 .HasColumnName("MealId")
                 .ValueGeneratedOnAdd();

            builder.Property(e => e.MealTime)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(e => e.UserId).IsRequired();
        }
    }
}
