using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using AutoMapper;
using Core.Business;
using Core.Business.Constants;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class TableManager : ITableService
{
    private readonly ITableDal _tableDal;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<TableCreateRequestDto> _createValidator;
    private readonly IValidator<TableUpdateRequestDto> _updateValidator;

    public TableManager(ITableDal tableDal, IMapper mapper, IUnitOfWork unitOfWork, IValidator<TableCreateRequestDto> createValidator, IValidator<TableUpdateRequestDto> updateValidator)
    {
        _tableDal = tableDal;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IDataResult<TableResponseDto>> AddAsync(TableCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);
        var exists = await _tableDal.AnyAsync(t => t.Name == dto.Name);
        if (exists)
        {
            return new ErrorDataResult<TableResponseDto>("Bu isimde masa zaten mevcut...");
        }

        var table = _mapper.Map<Table>(dto);
        table.IsDeleted = false;
        table.IsActive = true;

        await _tableDal.AddAsync(table);
        await _unitOfWork.CommitAsync();
        var response = _mapper.Map<TableResponseDto>(table);
        return new SuccessDataResult<TableResponseDto>(response, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(TableUpdateRequestDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var table = await _tableDal.GetAsync(t => t.Id == dto.Id);
        if (table is null)
        {
            return new ErrorDataResult<IResult>(ResultMessages.NotFound);
        }

        _mapper.Map(dto, table);
        _tableDal.Update(table);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var table = await _tableDal.GetAsync(t => t.Id == id && !t.IsDeleted);
        if (table is null)
        {
            return new ErrorDataResult<IResult>(ResultMessages.NotFound);
        }

        table.IsActive = true;
        table.IsDeleted = false;

        _tableDal.Update(table);
        await _unitOfWork.CommitAsync();
        return new ErrorResult(ResultMessages.SuccessDeleted);
    }

    public Task<IDataResult<TableResponseDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<TableResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<TableResponseDto>>> GetAllDeletedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<TableDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<TableDetailResponseDto>>> GetDetailAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<TableDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        throw new NotImplementedException();
    }
}
