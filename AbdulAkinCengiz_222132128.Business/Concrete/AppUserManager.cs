using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Users;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public class AppUserManager : IAppUserService
{
    public Task<AppUser> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<AppUserResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(AppUser user, string password)
    {
        throw new NotImplementedException();
    }
}
