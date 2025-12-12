using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Categories;
public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateRequestDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Kategory adı boş geçilemez...");
    }
}
