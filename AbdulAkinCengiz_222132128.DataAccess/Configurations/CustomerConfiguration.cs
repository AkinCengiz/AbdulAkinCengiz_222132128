using AbdulAkinCengiz_222132128.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.DataAccess.Configurations;
internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(c => c.LastName).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(50);
        builder.Property(c => c.Phone).HasColumnType("char(20)").IsRequired();
    }
}
