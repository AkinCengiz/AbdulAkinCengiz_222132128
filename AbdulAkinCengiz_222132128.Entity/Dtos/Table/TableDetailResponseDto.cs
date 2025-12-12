using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Table;

public sealed record TableDetailResponseDto : IDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte Seats { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}