using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Users;
public sealed record AppUserDetailResponseDto
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    public string PhoneNumber { get; init; }
    public string ImageUrl { get; init; }
}
