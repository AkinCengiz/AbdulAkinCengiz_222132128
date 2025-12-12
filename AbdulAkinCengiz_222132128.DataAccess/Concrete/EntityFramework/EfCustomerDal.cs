using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Contexts;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using Core.DataAccess;

namespace AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
public class EfCustomerDal : EfGenericRepository<Customer,AppDbContext>,ICustomerDal
{
    public EfCustomerDal(AppDbContext context) : base(context)
    {
    }
}
