using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Users;
public sealed record AppUserLoginRequestDto
{
    public string UserNameOrEmail { get; init; }
    public string Password { get; init; }
}
