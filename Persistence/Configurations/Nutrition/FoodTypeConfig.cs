using Domain.Nutrition;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations.Nutrition
{
    public class FoodTypeConfig : IEntityTypeConfiguration<FoodType>
    {
        public void Configure(EntityTypeBuilder<FoodType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("FoodTypeId")
                   .ValueGeneratedNever();

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.ImageUrl).HasMaxLength(100).IsRequired();

            builder.HasMany(e => e.Products)
                .WithOne(e => e.FoodType)
                .HasForeignKey(e => e.FoodTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
