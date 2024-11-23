using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Nutrition
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("ProductId")
                .ValueGeneratedNever();

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ProteinPer100).IsRequired();
            builder.Property(x => x.CarbsPer100).IsRequired();
            builder.Property(x => x.FatsPer100).IsRequired();
            builder.Property(x => x.CaloriesPer100).IsRequired();

            builder.HasOne(e => e.FoodType)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.FoodTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
