using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity;
public interface IDetailDto : IResponseDto
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
