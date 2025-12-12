using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Users;
public sealed record ResetPasswordRequestDto
{
    public string UserId { get; init; }
    public string Token { get; init; }
    public string NewPassword { get; init; }
}
