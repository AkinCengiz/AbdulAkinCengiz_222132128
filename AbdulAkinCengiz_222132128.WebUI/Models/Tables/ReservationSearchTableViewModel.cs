using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using System.ComponentModel.DataAnnotations;

namespace AbdulAkinCengiz_222132128.WebUI.Models.Tables;

public class ReservationSearchTableViewModel
{
    [Required(ErrorMessage = "Başlangıç tarih/saat zorunludur.")]
    public DateTime StartAt { get; set; }

    [Required(ErrorMessage = "Bitiş tarih/saat zorunludur.")]
    public DateTime EndAt { get; set; }

    [Required(ErrorMessage = "Misafir sayısı zorunludur.")]
    [Range(1, 50, ErrorMessage = "Misafir sayısı 1 ile 50 arasında olmalıdır.")]
    public byte GuestCount { get; set; }

    // Servisten gelen uygun masalar
    public List<TableResponseDto> AvailableTables { get; set; } = new();
}
