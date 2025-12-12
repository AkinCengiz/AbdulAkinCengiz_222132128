using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.OrderItems;
public class OrderItemUpdateDtoValidator : AbstractValidator<OrderItemUpdateRequestDto>
{
    public OrderItemUpdateDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0).WithMessage("OrderId geçerli olmalıdır");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId geçerli olmalıdır");

        RuleFor(x => x.Quantity)
            .GreaterThan((byte)0).WithMessage("Quantity 0'dan büyük olmalıdır");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("UnitPrice 0'dan büyük olmalıdır");
    }
}
