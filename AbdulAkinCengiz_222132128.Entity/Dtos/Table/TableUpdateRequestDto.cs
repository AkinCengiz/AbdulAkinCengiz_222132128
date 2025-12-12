using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Table;

public sealed record TableUpdateRequestDto : IUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Seats { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}