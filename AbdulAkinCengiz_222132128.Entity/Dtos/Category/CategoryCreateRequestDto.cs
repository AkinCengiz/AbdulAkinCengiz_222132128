using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Category;

public sealed record CategoryCreateRequestDto : ICreateDto
{
    public string Name { get; init; }
}