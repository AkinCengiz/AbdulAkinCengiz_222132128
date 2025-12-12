using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Validations.Categories;
public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateRequestDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Kategory adı boş geçilemez...");
    }
}
