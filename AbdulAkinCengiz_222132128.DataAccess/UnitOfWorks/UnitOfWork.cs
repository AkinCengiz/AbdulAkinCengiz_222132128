using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.DataAccess.Contexts;
using Core.UnitOfWorks;

namespace AbdulAkinCengiz_222132128.DataAccess.UnitOfWorks;
public class UnitOfWork(AppDbContext context):IUnitOfWork
{
    private readonly AppDbContext _context = context;
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}
