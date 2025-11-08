using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Concrete;

namespace AbdulAkinCengiz_222132128.DataAccess.Configurations;
internal class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(20).IsRequired();
        builder.Property(t => t.Seats).IsRequired();
    }
}
