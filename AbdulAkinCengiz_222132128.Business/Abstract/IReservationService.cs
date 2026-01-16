using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using Core.Business;
using Core.Utilities.Results;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IReservationService : IGenericService<Reservation,ReservationResponseDto,ReservationCreateRequestDto,ReservationUpdateRequestDto,ReservationDetailResponseDto>
{
    Task<IDataResult<IEnumerable<TableResponseDto>>> GetAvailableTablesAsync(
        DateTime startAt, DateTime endAt, byte guestCount);

    Task<IResult> CreateWithCustomerAsync(ReservationCreateWithCustomerRequestDto dto);
    Task<IDataResult<IEnumerable<ReservationResponseDto>>> GetTodayReservationAsync();
    Task<IResult> CheckInByTableAsync(int tableId);
    Task<int?> GetActiveOrderIdByTableAsync(int tableId);
    Task<IDataResult<int>> GetOrCreateActiveOrderIdByTableAsync(int tableId);
    Task<IDataResult<List<ReservationResponseDto>>> GetReservationsByTableIdAsync(int tableId);

    Task<IResult> UnCheckInByTableAsync(int tableId);
}
