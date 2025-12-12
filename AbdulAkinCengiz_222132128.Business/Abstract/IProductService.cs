using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using Core.Business;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IProductService : IGenericService<Product,ProductResponseDto,ProductCreateRequestDto,ProductUpdateRequestDto,ProductDetailResponseDto>
{
}
