using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using Core.Business;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface IProductService : IGenericService<Product,ProductResponseDto,ProductCreateRequestDto,ProductUpdateRequestDto,ProductDetailResponseDto>
{
    Task<IDataResult<List<ProductResponseDto>>> GetProductByCategoryIdAsync(int categoryId);
    Task<IDataResult<List<ProductResponseDto>>> GetAllForOrderAsync();
    Task<IDataResult<List<ProductResponseDto>>> GetProductsSortedByCategoryAsync();
    Task<IDataResult<List<ProductResponseDto>>> GetProductsSortedByNameAsync();
    Task<IDataResult<List<ProductResponseDto>>> GetProductsSortedByPriceAsync();
}
