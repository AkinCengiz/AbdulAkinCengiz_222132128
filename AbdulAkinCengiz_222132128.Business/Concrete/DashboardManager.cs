using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Contexts;
using AbdulAkinCengiz_222132128.Entity.Dtos.Dashboard;
using Core.Business.Constants;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Concrete;

public sealed class DashboardManager : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IDataResult<DashboardSummaryDto>> GetSummaryAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        var now = DateTime.Now;

        var activeOrderCount = await _context.Orders
            .AsNoTracking()
            .CountAsync(o => !o.IsDeleted && o.IsActive && !o.IsPaid);

        var openTableCount = await _context.Tables
            .AsNoTracking()
            .CountAsync(t =>
                !t.IsDeleted && t.IsActive
                             &&
                             !_context.Reservations.Any(r =>
                                 r.TableId == t.Id &&
                                 !r.IsDeleted &&
                                 r.IsActive &&
                                 r.IsConfirm &&
                                 r.StartAt <= now && now < r.EndAt
                             )
            );

        var todayReservationCount = await _context.Reservations
            .AsNoTracking()
            .CountAsync(r => !r.IsDeleted && r.IsActive && r.StartAt >= today && r.StartAt < tomorrow);

        var dto = new DashboardSummaryDto
        {
            ActiveOrderCount = activeOrderCount,
            OpenTableCount = openTableCount,
            TodayReservationCount = todayReservationCount
        };

        return new SuccessDataResult<DashboardSummaryDto>(dto, ResultMessages.SuccessListed);
    }

}
