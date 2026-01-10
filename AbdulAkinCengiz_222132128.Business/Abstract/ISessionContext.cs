using AbdulAkinCengiz_222132128.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;

public interface ISessionContext
{
    AppUser? CurrentUser { get; }
    void SetUser(AppUser user);
    void Clear();
}
