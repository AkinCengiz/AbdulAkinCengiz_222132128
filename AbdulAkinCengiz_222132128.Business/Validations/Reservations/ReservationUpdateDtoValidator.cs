using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Reservations;
public class ReservationUpdateDtoValidator : AbstractValidator<ReservationUpdateRequestDto>
{
    public ReservationUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Rezervasyon Id geçerli olmalıdır.");

        RuleFor(x => x.StartAt)
            .NotEmpty()
            .WithMessage("Rezervasyon başlangıç tarihi zorunludur.");

        RuleFor(x => x.EndAt)
            .NotEmpty()
            .GreaterThan(x => x.StartAt)
            .WithMessage("Rezervasyon bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

        RuleFor(x => x)
            .Must(HaveValidDuration)
            .WithMessage("Rezervasyon süresi en az 30 dakika olmalıdır.");

        RuleFor(x => x.GuestCount)
            .GreaterThan((byte)0)
            .WithMessage("Misafir sayısı 0'dan büyük olmalıdır.");

        RuleFor(x => x.TableId)
            .GreaterThan(0)
            .WithMessage("Geçerli bir masa seçilmelidir.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("Geçerli bir müşteri seçilmelidir.");

        // Silinmiş bir rezervasyon aktif olamaz
        RuleFor(x => x)
            .Must(x => !(x.IsDeleted && x.IsActive))
            .WithMessage("Silinmiş rezervasyon aktif olamaz.");

        // Güvenlik / iş kuralı (opsiyonel ama önerilir)
        RuleFor(x => x.IsConfirm)
            .Must(BeFalseOrManagedBySystem)
            .WithMessage("Rezervasyon onayı sistem tarafından yönetilir.");
    }

    private bool HaveValidDuration(ReservationUpdateRequestDto dto)
    {
        return (dto.EndAt - dto.StartAt).TotalMinutes >= 30;
    }

    private bool BeFalseOrManagedBySystem(bool isConfirm)
    {
        // Eğer onay süreci ayrı bir endpoint/komut ile yapılacaksa
        // update request üzerinden true gelmesine izin vermeyebilirsin
        return isConfirm == false;
    }
}
