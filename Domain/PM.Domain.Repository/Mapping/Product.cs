using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PM.Domain.Repository.Mapping;

public class Product : IEntityTypeConfiguration<Domain.Product.Model.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Product.Model.Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.StockQuantity);
    }
}