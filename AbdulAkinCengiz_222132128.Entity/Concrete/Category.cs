using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Entity.Concrete;
public sealed class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}