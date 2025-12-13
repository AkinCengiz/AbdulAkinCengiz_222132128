using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Tables;
public class TableUpdateDtoValidator : AbstractValidator<TableUpdateRequestDto>
{
    public TableUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Masa Id geçerli olmalıdır.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Masa adı zorunludur.")
            .MaximumLength(50).WithMessage("Masa adı en fazla 50 karakter olabilir.")
            .Must(n => n.Trim().Length > 0)
            .WithMessage("Masa adı sadece boşluklardan oluşamaz.");

        RuleFor(x => x.Seats)
            .GreaterThan((byte)0)
            .WithMessage("Kişi sayısı 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo((byte)20)
            .WithMessage("Kişi sayısı en fazla 20 olabilir.");

        // Silinmiş masa aktif olamaz
        RuleFor(x => x)
            .Must(x => !(x.IsDeleted && x.IsActive))
            .WithMessage("Silinmiş masa aktif olamaz.");
    }
}
