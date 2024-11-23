using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Nutrition
{
    public class ConsumedProductConfig : IEntityTypeConfiguration<ConsumedProduct>
    {
        public void Configure(EntityTypeBuilder<ConsumedProduct> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("ConsumedProductId")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.WeightGrams).IsRequired();

            builder.HasOne(e => e.Product)
                .WithMany(e => e.ConsumedProducts)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Meal)
                .WithMany(e => e.ConsumedProducts)
                .HasForeignKey(e => e.MealId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
