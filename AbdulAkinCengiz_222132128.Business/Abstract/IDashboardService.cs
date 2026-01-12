using AbdulAkinCengiz_222132128.Entity.Dtos.Dashboard;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;

public interface IDashboardService
{
    Task<IDataResult<DashboardSummaryDto>> GetSummaryAsync();
}
