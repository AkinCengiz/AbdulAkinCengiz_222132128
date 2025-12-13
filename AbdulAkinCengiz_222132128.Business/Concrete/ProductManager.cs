using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AutoMapper;
using Core.Business;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class ProductManager : IProductService
{
    public Task<IDataResult<ProductResponseDto>> AddAsync(ProductCreateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> UpdateAsync(ProductUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<ProductResponseDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<ProductResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<ProductResponseDto>>> GetAllDeletedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<ProductDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<ProductDetailResponseDto>>> GetDetailAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<ProductDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        throw new NotImplementedException();
    }
}
