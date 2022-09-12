using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiNet.Core.Entities.OrderAggregate;

namespace SkiNet.Infrastructure.Data.Config;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
  public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
  {
    builder.Property(d => d.Price).HasPrecision(18, 2);
  }
}