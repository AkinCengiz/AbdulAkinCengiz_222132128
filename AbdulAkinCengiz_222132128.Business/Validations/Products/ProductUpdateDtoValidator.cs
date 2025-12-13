using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Products;
public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Ürün Id geçerli olmalıdır.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(150).WithMessage("Ürün adı en fazla 150 karakter olabilir.")
            .Must(n => n.Trim().Length > 0).WithMessage("Ürün adı sadece boşluklardan oluşamaz.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0 veya daha büyük olmalıdır.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stok 0 veya daha büyük olmalıdır.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Geçerli bir kategori seçilmelidir.");
        RuleFor(x => x)
            .Must(x => !(x.IsDeleted && x.IsActive))
            .WithMessage("Silinmiş ürün aktif olamaz.");
    }
}
