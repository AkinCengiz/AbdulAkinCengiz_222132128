using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Orders;
public class OrderCreateDtoValidator : AbstractValidator<OrderCreateRequestDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(o => o.ReservationId).NotEmpty().WithMessage("Reservasyon id değeri boş geçilemez...");
    }
}
