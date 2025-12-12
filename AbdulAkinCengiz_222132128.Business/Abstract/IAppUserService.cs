using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Users;
using Core.Business;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IAppUserService
{
    Task<AppUser> GetByIdAsync(string id);
    Task<AppUser> GetByEmailAsync(string email);
    Task<IDataResult<IEnumerable<AppUserResponseDto>>> GetAllAsync();
    Task<IdentityResult> CreateAsync(AppUser user, string password);
}
