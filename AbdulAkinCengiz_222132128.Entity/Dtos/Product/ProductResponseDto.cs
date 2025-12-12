using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Product;
public sealed record ProductResponseDto : IResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public CategoryResponseDto Category { get; set; }
}