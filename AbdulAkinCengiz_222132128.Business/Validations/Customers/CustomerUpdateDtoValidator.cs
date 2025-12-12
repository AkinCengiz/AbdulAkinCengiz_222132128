using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Customers;
public class CustomerUpdateDtoValidator : AbstractValidator<CustomerUpdateRequestDto>
{
    public CustomerUpdateDtoValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("Ad alanı boş geçilemez...").MinimumLength(2).WithMessage("Adınız en az 2 karakterden oluşmalıdır...");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez...").MinimumLength(2).WithMessage("Soyadınız en az 2 karakterden oluşmalıdır...");
        RuleFor(c => c.Email).EmailAddress().WithMessage("Geçerli bir email girişi yapmalısınız..").NotEmpty().WithMessage("Email alanı boş geçilemez...");
        RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numarası boş olamaz")
            .Matches(@"^(\+90|0)?5\d{9}$").WithMessage("Telefon numarası geçerli formatta olmalıdır");
    }
}
