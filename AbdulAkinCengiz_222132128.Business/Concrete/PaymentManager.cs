using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
using AutoMapper;
using Core.Business.Constants;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class PaymentManager : IPaymentService
{
    private readonly IPaymentDal _paymentDal;
    private readonly IValidator<PaymentCreateRequestDto> _createValidator;
    private readonly IValidator<PaymentUpdateRequestDto> _updateValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaymentManager(IPaymentDal paymentDal, IValidator<PaymentCreateRequestDto> createValidator, IValidator<PaymentUpdateRequestDto> updateValidator, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _paymentDal = paymentDal;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IDataResult<PaymentResponseDto>> AddAsync(PaymentCreateRequestDto dto)
    {
        var isValid = await _createValidator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var errors = isValid.Errors.Select(e => e.ErrorMessage).ToList();
            return new ErrorDataResult<PaymentResponseDto>(String.Join(" | ",errors));
        }
        var entity = _mapper.Map<Payment>(dto);
        await _paymentDal.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        var responseDto = _mapper.Map<PaymentResponseDto>(entity);
        return new SuccessDataResult<PaymentResponseDto>(responseDto, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(PaymentUpdateRequestDto dto)
    {
        var isValid = await _updateValidator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var errors = isValid.Errors.Select(e => e.ErrorMessage).ToList();
            return new ErrorResult(String.Join(" | ", errors));
        }
        var entity = await _paymentDal.GetByIdAsync(dto.Id);
        if (entity == null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }
        _mapper.Map(dto, entity);
        _paymentDal.Update(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _paymentDal.GetByIdAsync(id);
        if (entity == null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }   
        _paymentDal.SoftDelete(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await _paymentDal.GetByIdAsync(id);
        if (entity == null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }
        _paymentDal.Remove(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<PaymentResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _paymentDal.GetByIdAsync(id);
        if (entity == null)
        {
            return new ErrorDataResult<PaymentResponseDto>(ResultMessages.NotFound);
        }

        var response = _mapper.Map<PaymentResponseDto>(entity);
        return new SuccessDataResult<PaymentResponseDto>(response, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<PaymentResponseDto>>> GetAllAsync()
    {
        var entities = await _paymentDal.GetAll(p => !p.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<PaymentResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<PaymentResponseDto>>(dtos, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<PaymentResponseDto>>> GetAllDeletedAsync()
    {
        var entities = await _paymentDal.GetAll(p => !p.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<PaymentResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<PaymentResponseDto>>(dtos, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<PaymentDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var entities = await _paymentDal.GetByIdAsync(id);
        if (entities == null)
        {
            return new ErrorDataResult<PaymentDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<PaymentDetailResponseDto>(entities);
        return new SuccessDataResult<PaymentDetailResponseDto>(dto, ResultMessages.SuccessGet);

    }

    public async Task<IDataResult<IEnumerable<PaymentDetailResponseDto>>> GetDetailAllAsync()
    {
        var entities = await _paymentDal.GetAll(p => !p.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<PaymentDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<PaymentDetailResponseDto>>(dtos, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<PaymentDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var entities = await _paymentDal.GetAll(p => p.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<PaymentDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<PaymentDetailResponseDto>>(dtos, ResultMessages.SuccessListed);
    }
}
