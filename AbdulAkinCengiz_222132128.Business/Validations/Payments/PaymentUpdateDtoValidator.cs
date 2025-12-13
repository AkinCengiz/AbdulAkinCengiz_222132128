using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Payments;
public class PaymentUpdateDtoValidator : AbstractValidator<PaymentUpdateRequestDto>
{
    public PaymentUpdateDtoValidator()
    {
        RuleFor(p => p.Id).NotEmpty().WithMessage("Id değeri zorunludur...");
        RuleFor(p => p.OrderId).NotEmpty().WithMessage("Sipariş id değeri boş geçilemez...");
        RuleFor(p => p.Amount).NotEmpty().WithMessage("Ödenecek tutar boş geçilemez...").InclusiveBetween(0, int.MaxValue).WithMessage("Ödenecek tutrar 0 dan büyük olmalıdır.");
        RuleFor(p => p.Method).NotEmpty().WithMessage("Lütfen ödeme yöntemini seçiniz...");
    }
}
