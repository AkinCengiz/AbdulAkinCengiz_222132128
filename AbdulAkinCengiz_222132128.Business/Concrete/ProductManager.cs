using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AutoMapper;
using Core.Business;
using Core.Business.Constants;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<ProductCreateRequestDto> _createValidator;
    private readonly IValidator<ProductUpdateRequestDto> _updateValidator;

    public ProductManager(IProductDal productDal, IMapper mapper, IUnitOfWork unitOfWork, IValidator<ProductCreateRequestDto> createValidator, IValidator<ProductUpdateRequestDto> updateValidator)
    {
        _productDal = productDal;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

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

    public Task<IResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<ProductResponseDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<IEnumerable<ProductResponseDto>>> GetAllAsync()
    {
        try
        {
            var products = _productDal
                .GetAll(p => !p.IsDeleted && p.IsActive)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToList();
            var dto = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            return new SuccessDataResult<IEnumerable<ProductResponseDto>>(dto, ResultMessages.SuccessListed);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<IEnumerable<ProductResponseDto>>(ResultMessages.ErrorListed);
        }
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

    public async Task<IDataResult<List<ProductResponseDto>>> GetProductByCategoryIdAsync(int categoryId)
    {
        if (categoryId <= 0)
            return new ErrorDataResult<List<ProductResponseDto>>("Geçersiz kategori.");

        try
        {
            // Soft delete / aktif filtreleri sizde varsa ekleyin
            var products = await _productDal
                .GetAll(p => !p.IsDeleted && p.IsActive && p.CategoryId == categoryId)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToListAsync();

            var dto = _mapper.Map<List<ProductResponseDto>>(products);

            return new SuccessDataResult<List<ProductResponseDto>>(dto, ResultMessages.SuccessListed);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<ProductResponseDto>>($"{ex.GetType().Name}: {ex.Message}");
        }
    }

    public async Task<IDataResult<List<ProductResponseDto>>> GetAllForOrderAsync()
    {
        var products = await _productDal
            .GetAll(p => !p.IsDeleted && p.IsActive)
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();

        var dto = _mapper.Map<List<ProductResponseDto>>(products);
        return new SuccessDataResult<List<ProductResponseDto>>(dto, ResultMessages.SuccessListed);
    }
}
