using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Users;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.AppUsers;
public class AppUserRegisterDtoValidator : AbstractValidator<AppUserRegisterRequestDto>
{
    public AppUserRegisterDtoValidator()
    {
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("Ad alanı boş geçilemez").MinimumLength(2).WithMessage("Adınız en az 2 karakterden oluşmalıdır...");
        RuleFor(a => a.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez").MinimumLength(2).WithMessage("Soyadınız en az 2 karakterden oluşmalıdır...");
        RuleFor(a => a.Email).EmailAddress().WithMessage("Geçerli bir email girişi yapmalısınız..").NotEmpty().WithMessage("Email alanı boş geçilemez...");
        RuleFor(a => a.UserName).MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter uzunluğunda olmalıdır...");
        RuleFor(a => a.Password).NotEmpty().WithMessage("Şifre boş olamaz")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir")
            .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir");
        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre tekrar alanı boş olamaz")
            .Equal(a => a.Password).WithMessage("Şifreler birbiriyle uyuşmuyor");
        RuleFor(a => a).Must(a => a.Password == a.ConfirmPassword).WithMessage("Şifre ve şifre tekrarı aynı olmalıdır");
    }
}
