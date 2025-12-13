using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Validations.Customers;

namespace AbdulAkinCengiz_222132128.Business.Validations.Reservations;
public sealed class ReservationCreateWithCustomerDtoValidator
    : AbstractValidator<ReservationCreateWithCustomerRequestDto>
{
    public ReservationCreateWithCustomerDtoValidator()
    {
        RuleFor(x => x.StartAt)
            .LessThan(x => x.EndAt)
            .WithMessage("Başlangıç tarihi bitiş tarihinden küçük olmalıdır.");

        RuleFor(x => x.GuestCount)
            .GreaterThan((byte)0);

        RuleFor(x => x.TableId)
            .GreaterThan(0);

        RuleFor(x => x.Customer)
            .NotNull();

        RuleFor(x => x.Customer)
            .SetValidator(new CustomerCreateDtoValidator());
    }
}
