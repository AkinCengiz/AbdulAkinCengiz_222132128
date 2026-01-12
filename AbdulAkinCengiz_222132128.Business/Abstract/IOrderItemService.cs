using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using Core.Business;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IOrderItemService : IGenericService<OrderItem, OrderItemResponseDto,OrderItemCreateRequestDto,OrderItemUpdateRequestDto,OrderItemDetailResponseDto>
{
    Task<IResult> AddOrIncreaseAsync(int orderId, int productId, byte quantity);

}
