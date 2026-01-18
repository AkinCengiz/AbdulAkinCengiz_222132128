using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using Core.Business;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IOrderService : IGenericService<Order,OrderResponseDto,OrderCreateRequestDto,OrderUpdateRequestDto,OrderDetailResponseDto>
{
    Task<IDataResult<int>> GetOrCreateActiveOrderByReservationAsync(int reservationId);
    Task<IDataResult<decimal>> SaveItemsAsync(int orderId, IEnumerable<OrderItemCreateRequestDto> items);
    Task<IDataResult<OrderContextDto>> GetContextAsync(int orderId);
    Task<IResult> PayAsync(int orderId);
    Task<IDataResult<OrderPaymentResponseDto>> GetPaymentContextByOrderIdAsync(int orderId);


}
