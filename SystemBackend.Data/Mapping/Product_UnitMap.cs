using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using SystemBackend.Entities;

namespace SystemBackend.Data.Mapping
{
    public class Product_UnitMap : IEntityTypeConfiguration<Product_Unit>
    {
        public void Configure(EntityTypeBuilder<Product_Unit> builder)
        {
            builder.ToTable("product_unit").HasKey(c => new { c.productId, c.unitId });
        }
    }
}
