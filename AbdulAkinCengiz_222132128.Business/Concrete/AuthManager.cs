using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private const string LoginProvider = "WinForms";
    private const string TokenName = "RememberMe";

    public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginResultDto> LoginAsync(LoginRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.UserNameOrEmail) || string.IsNullOrWhiteSpace(dto.Password))
            return LoginResultDto.Fail("Kullanıcı adı/e-posta ve şifre zorunludur.");

        // 1) userName veya email ile kullanıcı bul
        AppUser? user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);

        if (user == null && dto.UserNameOrEmail.Contains("@"))
            user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);

        if (user == null)
            return LoginResultDto.Fail("Kullanıcı bulunamadı.");

        // 2) Projedeki soft-state alanların (IsActive/IsDeleted) kontrolü
        if (!user.IsActive || user.IsDeleted)
            return LoginResultDto.Fail("Kullanıcı pasif veya silinmiş.");

        // 3) Şifre kontrolü + sign-in (cookie vs. WinForms için şart değil ama doğrulama için ideal)
        // lockoutOnFailure: true istersen lockout mekanizması çalışır
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);

        if (result.Succeeded)
            return LoginResultDto.Ok(user);

        if (result.IsLockedOut)
            return LoginResultDto.Fail("Hesap kilitlenmiş. Lütfen daha sonra tekrar deneyin.");

        return LoginResultDto.Fail("Kullanıcı adı/e-posta veya şifre hatalı.");
    }

    public async Task<(bool Success, string Message)> RequestPasswordResetAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return (false, "E-posta zorunludur.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, "Bu e-posta ile kullanıcı bulunamadı.");

        if (!user.IsActive || user.IsDeleted)
            return (false, "Kullanıcı pasif veya silinmiş.");

        // Token üret
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Web'de burada e-posta gönderirsin. WinForms'ta şimdilik mesaj gösterelim:
        // (Üretimde token'ı kesinlikle böyle göstermeyin; e-posta/SMS vb. ile iletin.)
        return (true, token);
    }

    public async Task<(bool Success, string Message)> ResetPasswordAsync(string email, string token, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(newPassword))
            return (false, "E-posta, token ve yeni şifre zorunludur.");

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return (false, "Kullanıcı bulunamadı.");

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.Description));
            return (false, errors);
        }

        return (true, "Şifre başarıyla güncellendi.");
    }

    public async Task<(bool Success, string Message)> RememberMeIssueTokenAsync(AppUser user)
    {
        // Rastgele token üret
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        // Identity UserTokens tablosuna yaz (AspNetUserTokens)
        await _userManager.SetAuthenticationTokenAsync(user, LoginProvider, TokenName, token);

        return (true, token);
    }

    public async Task<AppUser?> RememberMeValidateAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return null;

        if (!user.IsActive || user.IsDeleted) return null;

        var saved = await _userManager.GetAuthenticationTokenAsync(user, LoginProvider, TokenName);
        if (saved == null) return null;

        return saved == token ? user : null;
    }

    public async Task RememberMeRevokeAsync(AppUser user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(user, LoginProvider, TokenName);
    }


    public Task LogoutAsync()
    {
        return _signInManager.SignOutAsync();
    }
}
