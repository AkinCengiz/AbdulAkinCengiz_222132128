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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

        table.IsActive = false;
        table.IsDeleted = true;

        _tableDal.Update(table);
        await _unitOfWork.CommitAsync();
        return new ErrorResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<TableResponseDto>> GetByIdAsync(int id)
    {
        var table = await _tableDal.GetAsync(t => t.Id == id);
        if (table is null)
        {
            return new ErrorDataResult<TableResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<TableResponseDto>(table);
        return new SuccessDataResult<TableResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<TableResponseDto>>> GetAllAsync()
    {
        try
        {
            var query = _tableDal.GetAll(t => !t.IsDeleted).AsNoTracking();

            // 1) Buraya breakpoint koyun
            var sql = query.ToQueryString(); // EF Core 5+
            // sql değişkenini Watch'ta görün

            // 2) Buraya breakpoint koyun
            var tables = await query.ToListAsync();

            // 3) Buraya breakpoint koyun
            var dto = _mapper.Map<IEnumerable<TableResponseDto>>(tables);

            return new SuccessDataResult<IEnumerable<TableResponseDto>>(dto, ResultMessages.SuccessListed);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<IEnumerable<TableResponseDto>>($"{ex.GetType().Name}: {ex.Message}");
        }
        //var tables = await _tableDal.GetAll(t => !t.IsDeleted).ToListAsync();
        //var dto = _mapper.Map<IEnumerable<TableResponseDto>>(tables);
        //return new SuccessDataResult<IEnumerable<TableResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<TableResponseDto>>> GetAllDeletedAsync()
    {
        var tables = await _tableDal.GetAll(t => t.IsDeleted).ToListAsync();
        var dto = _mapper.Map<IEnumerable<TableResponseDto>>(tables);
        return new SuccessDataResult<IEnumerable<TableResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<TableDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var table = await _tableDal.GetAll(t => t.Id == id).Include(t => t.Reservations).FirstOrDefaultAsync();
        if (table is null)
        {
            return new ErrorDataResult<TableDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<TableDetailResponseDto>(table);
        return new SuccessDataResult<TableDetailResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<TableDetailResponseDto>>> GetDetailAllAsync()
    {
        var tables = await _tableDal.GetAll(t => !t.IsDeleted).Include(t => t.Reservations).ToListAsync();
        var dto = _mapper.Map<IEnumerable<TableDetailResponseDto>>(tables);
        return new SuccessDataResult<IEnumerable<TableDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<TableDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var tables = await _tableDal.GetAll(t => t.IsDeleted).Include(t => t.Reservations).ToListAsync();
        var dto = _mapper.Map<IEnumerable<TableDetailResponseDto>>(tables);
        return new SuccessDataResult<IEnumerable<TableDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<List<TableCardDto>>> GetTableCardsAsync(TableStatus? statusFilter = null, int reservedThresholdMinutes = 30)
    {
        var now = DateTime.Now;
        var reservedUntil = now.AddMinutes(reservedThresholdMinutes);

        var query = _tableDal.GetAll(t => !t.IsDeleted && t.IsActive)
            .AsNoTracking()
            .Select(t => new TableCardDto
            {
                TableId = t.Id,
                Name = t.Name,
                Seats = t.Seats,

                // Full: şu anda devam eden onaylı/aktif rezervasyon var
                Status = t.Reservations.Any(r =>
                    !r.IsDeleted && r.IsActive && r.IsConfirm &&
                    (r.StartAt <= now && now < r.EndAt) || r.IsCheckedIn)
                    ? TableStatus.Full
                    : (
                        // Reserved: şimdi yok ama X dakika içinde başlayacak rezervasyon var
                        t.Reservations.Any(r =>
                            !r.IsDeleted && r.IsActive && r.IsConfirm &&
                            now < r.StartAt && r.StartAt <= reservedUntil)
                            ? TableStatus.Reserved
                            : TableStatus.Empty
                    ),

                // En yakın gelecek rezervasyon başlangıcı (opsiyonel)
                NextReservationStartAt = t.Reservations
                    .Where(r =>
                        !r.IsDeleted && r.IsActive && r.IsConfirm &&
                        r.StartAt >= now && !r.IsCheckedIn)
                    .OrderBy(r => r.StartAt)
                    .Select(r => (DateTime?)r.StartAt)
                    .FirstOrDefault(),

                // Sipariş modülünüz netleşince dolduracağız
                ActiveOrderId = null
            });

        if (statusFilter.HasValue)
            query = query.Where(x => x.Status == statusFilter.Value);

        var list = await query
            .OrderBy(x => x.Name) // Section yoksa en mantıklısı
            .ToListAsync();

        return new SuccessDataResult<List<TableCardDto>>(list, ResultMessages.SuccessListed);
    }
}
