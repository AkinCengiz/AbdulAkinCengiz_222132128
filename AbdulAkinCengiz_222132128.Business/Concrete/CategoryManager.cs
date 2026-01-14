using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AutoMapper;
using Core.Business.Constants;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CategoryCreateRequestDto> _createValidator;
    private readonly IValidator<CategoryUpdateRequestDto> _updateValidator;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper, IUnitOfWork unitOfWork, IValidator<CategoryCreateRequestDto> createValidator, IValidator<CategoryUpdateRequestDto> updateValidator)
    {
        _repository = categoryDal;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IDataResult<CategoryResponseDto>> AddAsync(CategoryCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var exists = await _repository.AnyAsync(c => c.Name == dto.Name);
        if (exists)
        {
            return new ErrorDataResult<CategoryResponseDto>("Aynı isimde kategori zaten mevcut.");
        }

        var entity = _mapper.Map<Category>(dto);

        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CategoryResponseDto>(entity);
        return new SuccessDataResult<CategoryResponseDto>(response, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(CategoryUpdateRequestDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var entity = await _repository.GetAsync(c => c.Id == dto.Id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        var nameExists = await _repository.AnyAsync(c => c.Name == dto.Name);
        if (nameExists)
        {
            return new ErrorResult("Aynı isimde başka bir kategori mevcut.");
        }

        _mapper.Map(dto, entity);

        _repository.Update(entity);
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _repository.GetAsync(c => c.Id == id && !c.IsDeleted);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        _repository.SoftDelete(entity);
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);

    }

    public async Task<IDataResult<CategoryResponseDto>> GetByIdAsync(int id)
    {
        var category = await _repository.GetAsync(c => c.Id == id);

        if (category is null)
        {
            return new ErrorDataResult<CategoryResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<CategoryResponseDto>(category);
        return new SuccessDataResult<CategoryResponseDto>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CategoryResponseDto>>> GetAllAsync()
    {
        var categories = await _repository.GetAll(c => !c.IsDeleted).ToListAsync();

        var dto = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);

        return new SuccessDataResult<IEnumerable<CategoryResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CategoryResponseDto>>> GetAllDeletedAsync()
    {
        var categories = await _repository.GetAll(c => c.IsDeleted).ToListAsync();

        var dto = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);

        return new SuccessDataResult<IEnumerable<CategoryResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<CategoryDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var category = await _repository.GetAll(c => c.Id == id).Include(c => c.Products).FirstOrDefaultAsync();

        if (category is null)
        {
            return new ErrorDataResult<CategoryDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<CategoryDetailResponseDto>(category);
        return new SuccessDataResult<CategoryDetailResponseDto>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CategoryDetailResponseDto>>> GetDetailAllAsync()
    {
        var categories = await _repository.GetAll(c => !c.IsDeleted).Include(c => c.Products).ToListAsync();

        var dto = _mapper.Map<IEnumerable<CategoryDetailResponseDto>>(categories);

        return new SuccessDataResult<IEnumerable<CategoryDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CategoryDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var categories = await _repository.GetAll(c => c.IsDeleted).Include(c => c.Products).ToListAsync();

        var dto = _mapper.Map<IEnumerable<CategoryDetailResponseDto>>(categories);

        return new SuccessDataResult<IEnumerable<CategoryDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }
}
