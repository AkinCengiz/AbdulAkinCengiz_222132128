using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Users;
public sealed record AppUserUpdateRequestDto
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string ImageUrl { get; init; }
    public bool IsActive { get; init; }
    public bool IsDeleted { get; init; }
}
