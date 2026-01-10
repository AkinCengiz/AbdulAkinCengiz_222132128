using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;

public interface IAuthService
{
    Task<LoginResultDto> LoginAsync(LoginRequestDto dto);
    Task<(bool Success, string Message)> RequestPasswordResetAsync(string email);
    Task<(bool Success, string Message)> ResetPasswordAsync(string email, string token, string newPassword);
    Task<(bool Success, string Message)> RememberMeIssueTokenAsync(AppUser user);
    Task<AppUser?> RememberMeValidateAsync(string userId, string token);
    Task RememberMeRevokeAsync(AppUser user);
    Task LogoutAsync();
}
