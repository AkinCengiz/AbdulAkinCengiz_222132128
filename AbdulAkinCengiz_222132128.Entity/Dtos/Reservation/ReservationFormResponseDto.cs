using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;

public sealed class ReservationFormResponseDto
{
    public ReservationSearchTableDto Search { get; set; }
    public ReservationCreateWithCustomerRequestDto Create { get; set; }
}
