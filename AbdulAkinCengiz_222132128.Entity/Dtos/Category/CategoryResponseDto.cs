using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace AbdulAkinCengiz_222132128.Entity.Dtos.Category;
public sealed record CategoryResponseDto : IResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}