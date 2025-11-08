using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;
public interface IUpdateDto : IDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
