using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Users;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.AppUsers;
public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginRequestDto>
{
    public AppUserLoginDtoValidator()
    {
        RuleFor(a => a.UserNameOrEmail).NotNull().WithMessage("Kullanıcı adı ya da email alanı boş geçilemez...");
        RuleFor(a => a.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez...");
    }
}
