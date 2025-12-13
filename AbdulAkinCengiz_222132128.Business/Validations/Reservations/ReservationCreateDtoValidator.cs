using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Reservations;
public class ReservationCreateDtoValidator : AbstractValidator<ReservationCreateRequestDto>
{
    public ReservationCreateDtoValidator()
    {
        RuleFor(x => x.StartAt)
            .NotEmpty()
            .Must(BeInFuture)
            .WithMessage("Rezervasyon başlangıç tarihi geçmiş olamaz.");

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

    }
    private bool BeInFuture(DateTime startAt)
    {
        return startAt > DateTime.Now;
    }

    private bool HaveValidDuration(ReservationCreateRequestDto dto)
    {
        return (dto.EndAt - dto.StartAt).TotalMinutes >= 30;
    }
}
