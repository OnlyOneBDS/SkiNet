using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiNet.Core.Entities;

namespace SkiNet.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.Property(p => p.Name).IsRequired();
    builder.Property(p => p.Description).IsRequired();
    builder.Property(p => p.Price).HasColumnType("decimal").HasPrecision(18, 2);
    builder.Property(p => p.ImageUrl).IsRequired();
    builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
    builder.HasOne(b => b.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
  }
}