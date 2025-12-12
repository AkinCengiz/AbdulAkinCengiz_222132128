using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Table;

public sealed record TableCreateRequestDto : ICreateDto
{
    public string Name { get; set; }
    public byte Seats { get; set; }
}