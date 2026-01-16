using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
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
using System.ComponentModel;
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

    public async Task<IDataResult<ProductResponseDto>> AddAsync(ProductCreateRequestDto dto)
    {
        var validate = await _createValidator.ValidateAsync(dto);
        if (!validate.IsValid)
        {
            var errors = validate.Errors
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToList();
            return new ErrorDataResult<ProductResponseDto>(String.Join(" | ", errors));
        }

        var exists = await _productDal.AnyAsync(p => p.Name == dto.Name);
        if (exists)
        {
            return new ErrorDataResult<ProductResponseDto>("Aynı isimde ürün zaten mevcut...");
        }

        var entity = _mapper.Map<Product>(dto);
        await _productDal.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        var response = _mapper.Map<ProductResponseDto>(entity);
        return new SuccessDataResult<ProductResponseDto>(response, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(ProductUpdateRequestDto dto)
    {
        var isValid = await _updateValidator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var errors = isValid.Errors.Select(e => e.ErrorMessage).Distinct().ToList();
            return new ErrorDataResult<ProductResponseDto>(String.Join(" | ", errors));
        }

        var entity = await _productDal.GetByIdAsync(dto.Id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        _mapper.Map(dto, entity);
        _productDal.Update(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _productDal.GetByIdAsync(id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }
        _productDal.SoftDelete(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await _productDal.GetByIdAsync(id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }
        _productDal.Remove(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<ProductResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _productDal.GetByIdAsync(id);

        if (entity is null)
        {
            return new ErrorDataResult<ProductResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<ProductResponseDto>(entity);
        return new SuccessDataResult<ProductResponseDto>(dto, ResultMessages.SuccessGet);
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

    public async Task<IDataResult<IEnumerable<ProductResponseDto>>> GetAllDeletedAsync()
    {
        try
        {
            var products = _productDal
                .GetAll(p => p.IsDeleted)
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

    public async Task<IDataResult<ProductDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var entity = await _productDal.GetByIdAsync(id);

        if (entity is null)
        {
            return new ErrorDataResult<ProductDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<ProductDetailResponseDto>(entity);
        return new SuccessDataResult<ProductDetailResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<ProductDetailResponseDto>>> GetDetailAllAsync()
    {
        try
        {
            var products = _productDal
                .GetAll(p => !p.IsDeleted && p.IsActive)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToList();
            var dto = _mapper.Map<IEnumerable<ProductDetailResponseDto>>(products);
            return new SuccessDataResult<IEnumerable<ProductDetailResponseDto>>(dto, ResultMessages.SuccessListed);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<IEnumerable<ProductDetailResponseDto>>(ResultMessages.ErrorListed);
        }
    }

    public async Task<IDataResult<IEnumerable<ProductDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        try
        {
            var products = _productDal
                .GetAll(p => !p.IsDeleted)
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .ToList();
            var dto = _mapper.Map<IEnumerable<ProductDetailResponseDto>>(products);
            return new SuccessDataResult<IEnumerable<ProductDetailResponseDto>>(dto, ResultMessages.SuccessListed);
        }
        catch (Exception e)
        {
            return new ErrorDataResult<IEnumerable<ProductDetailResponseDto>>(ResultMessages.ErrorListed);
        }
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
