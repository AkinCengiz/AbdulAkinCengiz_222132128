using AbdulAkinCengiz_222132128.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.DataAccess.Configurations;
internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(r => r.StartAt).IsRequired();
        builder.Property(r => r.EndAt).IsRequired();
        builder.Property(r => r.GuestCount).IsRequired();
    }
}
