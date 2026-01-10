using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;

namespace AbdulAkinCengiz_222132128.Business.Concrete;

public class SessionContext : ISessionContext
{
    public AppUser? CurrentUser { get; private set; }
    public void SetUser(AppUser user)
    {
        CurrentUser = user;
    } 

    public void Clear()
    {
        CurrentUser = null;
    }
}
