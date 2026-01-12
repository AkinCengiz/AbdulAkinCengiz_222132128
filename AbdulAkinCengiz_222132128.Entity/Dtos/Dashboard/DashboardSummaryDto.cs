using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Dashboard;

public sealed class DashboardSummaryDto
{
    public int OpenTableCount { get; set; }
    public int ActiveOrderCount { get; set; }
    public int TodayReservationCount { get; set; }
}
