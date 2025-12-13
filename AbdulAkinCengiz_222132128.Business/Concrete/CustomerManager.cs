using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
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
public sealed class CustomerManager : ICustomerService
{
    private readonly ICustomerDal _customerDal;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CustomerCreateRequestDto> _createValidator;
    private readonly IValidator<CustomerUpdateRequestDto> _updateValidator;

    public CustomerManager(ICustomerDal customerDal, IMapper mapper, IUnitOfWork unitOfWork, IValidator<CustomerCreateRequestDto> createValidator, IValidator<CustomerUpdateRequestDto> updateValidator)
    {
        _customerDal = customerDal;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<Customer> GetOrCreateCustomerAsync(CustomerCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var existing = await _customerDal.GetAsync(c => c.Phone == dto.Phone);
        if (existing is not null)
        {
            return existing;
        }

        var customer = _mapper.Map<Customer>(dto);
        await _customerDal.AddAsync(customer);

        return customer;
    }

    public async Task<IDataResult<CustomerResponseDto>> AddAsync(CustomerCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var emailExists = await _customerDal.AnyAsync(c => c.Email == dto.Email);
        if (emailExists)
        {
            return new ErrorDataResult<CustomerResponseDto>("Bu e-posta ile kayıtlı müşteri zaten var.");
        }

        var phoneExists = await _customerDal.AnyAsync(c => c.Phone == dto.Phone);
        if (phoneExists)
        {
            return new ErrorDataResult<CustomerResponseDto>("Bu telefon ile kayıtlı müşteri zaten var.");
        }

        var entity = _mapper.Map<Customer>(dto);
        entity.IsActive = true;
        entity.IsDeleted = false;

        await _customerDal.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CustomerResponseDto>(entity);
        return new SuccessDataResult<CustomerResponseDto>(response, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(CustomerUpdateRequestDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var entity = await _customerDal.GetAsync(c => c.Id == dto.Id);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        _mapper.Map(dto, entity);

        _customerDal.Update(entity);
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _customerDal.GetAsync(c => c.Id == id && !c.IsDeleted);
        if (entity is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        entity.IsDeleted = true;
        entity.IsActive = false;

        _customerDal.Update(entity);
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<CustomerResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _customerDal.GetAsync(c => c.Id == id);
        if (entity is null)
        {
            return new ErrorDataResult<CustomerResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<CustomerResponseDto>(entity);
        return new SuccessDataResult<CustomerResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<CustomerResponseDto>>> GetAllAsync()
    {
        var customers = await _customerDal.GetAll(c => !c.IsDeleted).ToListAsync();
        var dto = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

        return new SuccessDataResult<IEnumerable<CustomerResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CustomerResponseDto>>> GetAllDeletedAsync()
    {
        var customers = await _customerDal.GetAll(c => c.IsDeleted).ToListAsync();
        var dto = _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);

        return new SuccessDataResult<IEnumerable<CustomerResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<CustomerDetailResponseDto>> GetDetailByIdAsync(int id)
    {

        var customer = await _customerDal.GetAll(c => c.Id == id).Include(c => c.Reservations).FirstOrDefaultAsync();


        if (customer is null)
        {
            return new ErrorDataResult<CustomerDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<CustomerDetailResponseDto>(customer);
        return new SuccessDataResult<CustomerDetailResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<CustomerDetailResponseDto>>> GetDetailAllAsync()
    {
        var customers = await _customerDal.GetAll(c => !c.IsDeleted).Include(c => c.Reservations).ToListAsync();
        
        var dto = _mapper.Map<IEnumerable<CustomerDetailResponseDto>>(customers);

        return new SuccessDataResult<IEnumerable<CustomerDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<CustomerDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var customers = await _customerDal.GetAll(c => c.IsDeleted).Include(c => c.Reservations).ToListAsync();

        var dto = _mapper.Map<IEnumerable<CustomerDetailResponseDto>>(customers);

        return new SuccessDataResult<IEnumerable<CustomerDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }
}
