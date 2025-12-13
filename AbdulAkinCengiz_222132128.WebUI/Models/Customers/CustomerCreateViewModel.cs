namespace AbdulAkinCengiz_222132128.WebUI.Models.Customers;

public sealed class CustomerCreateViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
}
