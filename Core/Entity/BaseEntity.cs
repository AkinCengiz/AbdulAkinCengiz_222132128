using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;
public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}
