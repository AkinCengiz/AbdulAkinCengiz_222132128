using AbdulAkinCengiz_222132128.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.DataAccess.Configurations;
internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(o => o.UnitPrice).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Quantity).HasDefaultValue(1);
    }
}
