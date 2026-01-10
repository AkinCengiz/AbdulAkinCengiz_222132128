using AbdulAkinCengiz_222132128.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Auth;

public sealed class LoginRequestDto
{
    public string UserNameOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public sealed class LoginResultDto
{
    public bool Success { get; init; }
    public string Message { get; init; } = default!;
    public AppUser? User { get; init; }

    public static LoginResultDto Ok(AppUser user) =>
        new() { Success = true, Message = "Giriş başarılı.", User = user };

    public static LoginResultDto Fail(string message) =>
        new() { Success = false, Message = message };
}
