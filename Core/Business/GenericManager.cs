using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.Constants;
using Core.DataAccess;
using Core.Entity;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;

namespace Core.Business;
public abstract class GenericManager<TEntity,TResponse,TCreate,TUpdate,TDetail> 
    //: IGenericService<TEntity, TResponse, TCreate, TUpdate, TDetail>
where TEntity : class, IEntity, new()
where TResponse : class,IResponseDto,new()
where TCreate : class,ICreateDto, new()
where TUpdate : class,IUpdateDto, new()
where TDetail : class,IDetailDto, new()
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    protected readonly IValidator<TCreate>? _createValidator;
    protected readonly IValidator<TUpdate>? _updateValidator;

    protected GenericManager(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<TCreate>? createValidator = null, IValidator<TUpdate>? updateValidator = null)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public virtual async Task<IDataResult<TResponse>> AddAsync(TCreate dto)
    {
        if (_createValidator is not null)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
        }

        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<TResponse>(entity);
        return new SuccessDataResult<TResponse>(response,ResultMessages.SuccessCreated);
    }

    public virtual async Task<IResult> UpdateAsync(TUpdate dto)
    {
        if (_updateValidator is not null)
            await _updateValidator.ValidateAndThrowAsync(dto);

        if (dto.Id <= 0)
        {
            return new ErrorResult(ResultMessages.InvalidId);
        }

        var entity = await _repository.GetAsync(e => e.Id == dto.Id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        _mapper.Map(dto, entity);

        _repository.Update(entity);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<TResponse>(entity);
        return new SuccessDataResult<TResponse>(response, ResultMessages.SuccessUpdated);
    }

    public virtual async Task<IResult> RemoveAsync(int id)
    {
        if (id <= 0)
        {
            return new ErrorResult(ResultMessages.InvalidId);
        }
        var entity = await _repository.GetAsync(e => e.Id == id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        if (entity.IsDeleted)
        {
            return new ErrorResult(ResultMessages.AlreadyDeleted);
        }
        entity.IsActive = false;
        entity.IsDeleted = true;
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public virtual async Task<IDataResult<TResponse>> GetByIdAsync(int id, bool includeDeleted = false)
    {
        if (id <= 0)
        {
            return new ErrorDataResult<TResponse>(ResultMessages.InvalidId);
        }

        var entity = await _repository.GetAsync(e => e.Id == id);
        if (entity is null)
        {
            return new ErrorDataResult<TResponse>(ResultMessages.NotFound);
        }

        if (!includeDeleted && entity.IsDeleted)
        {
            return new ErrorDataResult<TResponse>(ResultMessages.NotFound);
        }

        var response = _mapper.Map<TResponse>(entity);
        return new SuccessDataResult<TResponse>(response, ResultMessages.SuccessGet);
    }

    public virtual async Task<IDataResult<IEnumerable<TResponse>>> GetAllAsync(bool includeDeleted = false, bool onlyDeleted = false)
    {
        if (includeDeleted && onlyDeleted)
        {
            return new ErrorDataResult<IEnumerable<TResponse>>("İki parametre aynı anda doğru olamaz.");
        }
        var query = _repository.GetAll();
        if (onlyDeleted)
        {
            query = query.Where(e => e.IsDeleted);
        }
        else if (!includeDeleted)
        {
            query = query.Where(e => !e.IsDeleted);
        }

        var entities = await query.ToListAsync();

        var response = _mapper.Map<IEnumerable<TResponse>>(entities);
        return new SuccessDataResult<IEnumerable<TResponse>>(response, ResultMessages.SuccessListed);
    }

    public virtual async Task<IDataResult<TDetail>> GetDetailByIdAsync(int id, bool includeDeleted = false)
    {
        if (id <= 0)
        {
            return new ErrorDataResult<TDetail>(ResultMessages.InvalidId);
        }

        var entity = await _repository.GetAsync(e => e.Id == id);
        if (entity is null)
        {
            return new ErrorDataResult<TDetail>(ResultMessages.NotFound);
        }

        if (!includeDeleted && entity.IsDeleted)
        {
            return new ErrorDataResult<TDetail>(ResultMessages.NotFound);
        }

        var response = _mapper.Map<TDetail>(entity);
        return new SuccessDataResult<TDetail>(response, ResultMessages.SuccessGet);
    }

    public virtual async Task<IDataResult<IEnumerable<TDetail>>> GetDetailAllAsync(bool includeDeleted = false, bool onlyDeleted = false)
    {
        if (includeDeleted && onlyDeleted)
        {
            return new ErrorDataResult<IEnumerable<TDetail>>("İki parametre aynı anda doğru olamaz.");
        }
        var query = _repository.GetAll();
        if (onlyDeleted)
        {
            query = query.Where(e => e.IsDeleted);
        }
        else if (!includeDeleted)
        {
            query = query.Where(e => !e.IsDeleted);
        }

        var entities = await query.ToListAsync();

        var response = _mapper.Map<IEnumerable<TDetail>>(entities);
        return new SuccessDataResult<IEnumerable<TDetail>>(response, ResultMessages.SuccessListed);
    }
}
