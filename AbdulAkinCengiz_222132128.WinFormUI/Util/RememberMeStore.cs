using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.WinFormUI.Util;

public static class RememberMeStore
{
    private static string FilePath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YourAppName", "remember.dat");

    public static void Save(string userId, string token)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        var payload = $"{userId}|{token}";
        var bytes = Encoding.UTF8.GetBytes(payload);

        // DPAPI - CurrentUser scope
        var protectedBytes = ProtectedData.Protect(bytes, optionalEntropy: null, DataProtectionScope.CurrentUser);
        File.WriteAllBytes(FilePath, protectedBytes);
    }

    public static (string UserId, string Token)? Load()
    {
        if (!File.Exists(FilePath)) return null;

        var protectedBytes = File.ReadAllBytes(FilePath);
        var bytes = ProtectedData.Unprotect(protectedBytes, optionalEntropy: null, DataProtectionScope.CurrentUser);

        var payload = Encoding.UTF8.GetString(bytes);
        var parts = payload.Split('|');
        if (parts.Length != 2) return null;

        return (parts[0], parts[1]);
    }

    public static void Clear()
    {
        if (File.Exists(FilePath))
            File.Delete(FilePath);
    }
}
