using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AutoMapper;
using Core.Business;
using Core.Business.Constants;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Validations.Reservations;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using Microsoft.EntityFrameworkCore;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class ReservationManager : IReservationService
{
    private readonly IReservationDal _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICustomerService _customerService;
    private readonly ICustomerDal _customerDal;
    private readonly ITableDal _tableDal;
    private readonly IOrderDal _orderDal;
    private readonly IValidator<ReservationCreateWithCustomerRequestDto> _withCustomerValidator;
    private readonly IValidator<ReservationCreateRequestDto> _createValidator;
    private readonly IValidator<ReservationUpdateRequestDto> _updateValidator;


    public ReservationManager(IReservationDal repository, IMapper mapper, IUnitOfWork unitOfWork, ICustomerService customerService, ITableDal tableDal, IValidator<ReservationCreateWithCustomerRequestDto> withCustomerValidator, IValidator<ReservationCreateRequestDto> createValidator, ICustomerDal customerDal, IValidator<ReservationUpdateRequestDto> updateValidator, IOrderDal orderDal)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _customerService = customerService;
        _tableDal = tableDal;
        _withCustomerValidator = withCustomerValidator;
        _createValidator = createValidator;
        _customerDal = customerDal;
        _updateValidator = updateValidator;
        _orderDal = orderDal;
    }

    public async Task<IResult> CreateWithCustomerAsync(ReservationCreateWithCustomerRequestDto dto)
    {
        await _withCustomerValidator.ValidateAndThrowAsync(dto);

        var table = await _tableDal.GetAsync(t => t.Id == dto.TableId);
        if (table is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        if (!table.IsActive || table.IsDeleted)
        {
            return new ErrorResult("Masa şuan aktif değil...");
        }

        if (dto.GuestCount > table.Seats)
        {
            return new ErrorResult("Misafir sayısı için masa kapasitesi yetersiz...");
        }

        var hasOverlap = await _repository.GetAll().Where(r => r.TableId == table.Id && r.IsActive && !r.IsDeleted)
            .AnyAsync(r => r.StartAt < dto.EndAt && r.EndAt > dto.StartAt);

        if (hasOverlap)
        {
            return new ErrorResult("Seçilen tarih ve saat aralığında masa dolu...");
        }

        var customerDto = _mapper.Map<CustomerCreateRequestDto>(dto.Customer);

        var customer = await _customerService.GetOrCreateCustomerAsync(customerDto);

        //var reservation = new Reservation
        //{
        //    StartAt = dto.StartAt,
        //    EndAt = dto.EndAt,
        //    GuestCount = dto.GuestCount,
        //    TableId = dto.TableId,
        //    Customer = customer,
        //    IsConfirm = false
        //};
        var reservation = _mapper.Map<Reservation>(dto);
        reservation.IsConfirm = false;
        reservation.CustomerId = customer.Id;

        await _repository.AddAsync(reservation);

        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessCreated);
    }

    public async Task<IDataResult<IEnumerable<ReservationResponseDto>>> GetTodayReservationAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        var query = _repository.GetAll(r => !r.IsDeleted && r.StartAt >= today && r.StartAt < tomorrow).AsNoTracking()
            .OrderBy(r => r.StartAt).Select(r => new ReservationResponseDto()
            {
                Id = r.Id,
                StartAt = r.StartAt,
                EndAt = r.EndAt,
                GuestCount = r.GuestCount,
                IsConfirm = r.IsConfirm,
                Table = new TableResponseDto
                {
                    Id = r.Table.Id,
                    Name = r.Table.Name,
                    Seats = r.Table.Seats,
                },
                Customer = new CustomerResponseDto
                {
                    Id = r.Customer.Id,
                    FirstName = r.Customer.FirstName,
                    LastName = r.Customer.LastName,
                    Phone = r.Customer.Phone,
                }
            });
        var reservationList = await query.ToListAsync();
        return new SuccessDataResult<IEnumerable<ReservationResponseDto>>(reservationList, ResultMessages.SuccessListed);
    }

    public async Task<IResult> CheckInByTableAsync(int tableId)
    {
        if (tableId <= 0)
            return new ErrorResult("Geçersiz masa.");

        var now = DateTime.Now;

        // 1) Masa dolu mu?
        // Yani: Bu masaya bağlı, check-in yapılmış ve ödenmemiş bir sipariş var mı?
        var isBusy = await _repository.GetAll(r =>
                r.TableId == tableId &&
                r.IsCheckedIn &&
                r.IsActive &&
                !r.IsDeleted)
            .AnyAsync(r =>
                r.Order != null &&
                r.Order.IsActive &&
                !r.Order.IsPaid &&
                !r.Order.IsDeleted);

        if (isBusy)
            return new ErrorResult("Masa zaten dolu. Check-in yapılamaz.");

        // 2) Bu masanın check-in yapılmamış en yakın rezervasyonu
        var reservation = await _repository.GetAll(r =>
                r.TableId == tableId &&
                r.IsConfirm &&
                r.IsActive &&
                !r.IsDeleted &&
                !r.IsCheckedIn)
            .OrderBy(r => r.StartAt)
            .FirstOrDefaultAsync();

        if (reservation is null)
            return new ErrorResult("Bu masa için check-in yapılacak rezervasyon bulunamadı.");

        // 3) Check-in
        reservation.IsCheckedIn = true;
        reservation.CheckInAt = now;

        _repository.Update(reservation);

        // 4) Eğer bu rezervasyona ait Order yoksa oluştur
        if (reservation.Order == null)
        {
            var order = new Order
            {
                ReservationId = reservation.Id,
                IsActive = true,
                IsDeleted = false,
                IsPaid = false,
                CreateAt = now
            };

            await _orderDal.AddAsync(order);
            await _unitOfWork.CommitAsync();
        }

        await _unitOfWork.CommitAsync();

        return new SuccessResult("Check-in yapıldı. Masa dolu duruma geçti.");
    }

    public async Task<int?> GetActiveOrderIdByTableAsync(int tableId)
    {
        if (tableId <= 0) return null;

        // 1) Bu masada aktif (check-in) rezervasyonu bul
        // 2) Order varsa OrderId'yi döndür
        var orderId = await _repository.GetAll(r =>
                r.TableId == tableId &&
                r.IsCheckedIn &&
                r.IsActive &&
                !r.IsDeleted)
            .Where(r => r.Order != null)               // 🔴 kritik: null filtre
            .Select(r => (int?)r.Order.Id)             // 🔴 kritik: nullable select
            .FirstOrDefaultAsync();

        return orderId; // null olabilir
    }

    public async Task<IDataResult<int>> GetOrCreateActiveOrderIdByTableAsync(int tableId)
    {
        if (tableId <= 0)
            return new ErrorDataResult<int>("Geçersiz masa.");

        var now = DateTime.Now;

        // 1) Bu masada check-in yapılmış aktif rezervasyonu bul
        // Not: Bir masada aynı anda 1 aktif check-in olmalı varsayımı.
        var reservation = await _repository.GetAll(r =>
                r.TableId == tableId &&
                r.IsCheckedIn &&
                r.IsActive &&
                !r.IsDeleted)
            .Include(r => r.Order) // one-to-one order
            .OrderByDescending(r => r.CheckInAt) // en güncel
            .FirstOrDefaultAsync();

        if (reservation is null)
            return new ErrorDataResult<int>("Bu masada check-in yapılmış aktif rezervasyon yok.");

        // 2) Order varsa direkt dön
        if (reservation.Order != null && reservation.Order.IsActive && !reservation.Order.IsDeleted && !reservation.Order.IsPaid)
            return new SuccessDataResult<int>(reservation.Order.Id, "Sipariş bulundu.");

        // 3) Order yoksa (veya pasif/ödenmişse) yeni Order oluştur
        var order = new Order
        {
            ReservationId = reservation.Id,
            IsActive = true,
            IsDeleted = false,
            IsPaid = false,
            CreateAt = now
        };

        await _orderDal.AddAsync(order);
        await _unitOfWork.CommitAsync();

        return new SuccessDataResult<int>(order.Id, "Sipariş oluşturuldu.");
    }

    public async Task<IDataResult<IEnumerable<TableResponseDto>>> GetAvailableTablesAsync(
        DateTime startAt, DateTime endAt, byte guestCount)
    {
        if (guestCount <= 0)
        {
            return new ErrorDataResult<IEnumerable<TableResponseDto>>("Misafir sayısı 0 olamaz.");
        }

        if (startAt >= endAt)
        {
            return new ErrorDataResult<IEnumerable<TableResponseDto>>("Başlangıç tarihi bitiş tarihinden küçük olmalıdır.");
        }

        // Aktif, silinmemiş ve kapasitesi yeterli masalar
        var tablesQuery = _tableDal.GetAll()
            .Where(t => t.IsActive && !t.IsDeleted)
            .Where(t => t.Seats >= guestCount);

        // İstenen aralıkla çakışan (overlap) aktif rezervasyonlar
        var overlappingReservations = _repository.GetAll()
            .Where(r => r.IsActive && !r.IsDeleted)
            .Where(r => r.StartAt < endAt && r.EndAt > startAt);

        // Çakışan rezervasyonu olan masaları dışla
        var availableTables = await tablesQuery
            .Where(t => !overlappingReservations.Any(r => r.TableId == t.Id))
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<TableResponseDto>>(availableTables);

        return new SuccessDataResult<IEnumerable<TableResponseDto>>(response, ResultMessages.SuccessListed);
    }


    public async Task<IDataResult<ReservationResponseDto>> AddAsync(
        ReservationCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var table = await _tableDal.GetAsync(t => t.Id == dto.TableId);
        if (table is null)
        {
            return new ErrorDataResult<ReservationResponseDto>(ResultMessages.NotFound);
        }

        if (dto.GuestCount > table.Seats)
        {
            return new ErrorDataResult<ReservationResponseDto>("Misafir sayısı için masa kapasitesi yetersiz.");
        }

        var customerExists = await _customerDal.AnyAsync(c => c.Id == dto.CustomerId);
        if (!customerExists)
        {
            return new ErrorDataResult<ReservationResponseDto>("Müşteri bulunamadı.");
        }

        var hasOverlap = await _repository.GetAll()
            .Where(r => r.TableId == table.Id && r.IsActive && !r.IsDeleted)
            .AnyAsync(r => r.StartAt < dto.EndAt && r.EndAt > dto.StartAt);

        if (hasOverlap)
        {
            return new ErrorDataResult<ReservationResponseDto>("Seçilen tarih ve saat aralığında masa dolu.");
        }


        var reservation = _mapper.Map<Reservation>(dto);
        reservation.IsConfirm = false;
        reservation.IsActive = true;
        reservation.IsDeleted = false;


        await _repository.AddAsync(reservation);
        await _unitOfWork.CommitAsync();


        var responseDto = _mapper.Map<ReservationResponseDto>(reservation);

        return new SuccessDataResult<ReservationResponseDto>(
            responseDto,
            ResultMessages.SuccessCreated);
    }


    public async Task<IResult> UpdateAsync(ReservationUpdateRequestDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var reservation = await _repository.GetAsync(r => r.Id == dto.Id);
        if (reservation is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        var table = await _tableDal.GetAsync(t => t.Id == dto.TableId);
        if (table is null)
        {
            return new ErrorResult(ResultMessages.NotFound);
        }

        if (dto.GuestCount > table.Seats)
        {
            return new ErrorResult("Misafir sayısı için masa kapasitesi yetersiz...");
        }

        // aynı masa için, kendi kaydı hariç overlap kontrolü
        var hasOverlap = await _repository.GetAll()
            .Where(r => r.TableId == table.Id && r.IsActive && !r.IsDeleted && r.Id != dto.Id)
            .AnyAsync(r => r.StartAt < dto.EndAt && r.EndAt > dto.StartAt);

        if (hasOverlap)
        {
            return new ErrorResult("Seçilen tarih ve saat aralığında masa dolu...");
        }

        reservation.StartAt = dto.StartAt;
        reservation.EndAt = dto.EndAt;
        reservation.GuestCount = dto.GuestCount;
        reservation.TableId = dto.TableId;
        reservation.IsConfirm = dto.IsConfirm;

        _repository.Update(reservation);

        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }


    public async Task<IResult> RemoveAsync(int id)
    {
        var reservation = await _repository.GetAsync(r => r.Id == id && !r.IsDeleted);
        if (reservation is null)
            return new ErrorResult(ResultMessages.NotFound);

        reservation.IsDeleted = true;
        reservation.IsActive = false;

        // _repository.Update(reservation); // varsa
        await _unitOfWork.CommitAsync();

        return new SuccessResult(ResultMessages.SuccessDeleted);
    }


    public async Task<IDataResult<ReservationResponseDto>> GetByIdAsync(int id)
    {
        var reservation = await _repository.GetAsync(r => r.Id == id);

        if (reservation is null)
        {
            return new ErrorDataResult<ReservationResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<ReservationResponseDto>(reservation);
        return new SuccessDataResult<ReservationResponseDto>(dto, ResultMessages.SuccessListed);
    }


    public async Task<IDataResult<IEnumerable<ReservationResponseDto>>> GetAllAsync()
    {
        var reservatios = await _repository.GetAll(r => !r.IsDeleted).ToListAsync();

        var dto = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservatios);

        return new SuccessDataResult<IEnumerable<ReservationResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<ReservationResponseDto>>> GetAllDeletedAsync()
    {
        var reservatios = await _repository.GetAll(r => r.IsDeleted).ToListAsync();

        var dto = _mapper.Map<IEnumerable<ReservationResponseDto>>(reservatios);

        return new SuccessDataResult<IEnumerable<ReservationResponseDto>>(dto, ResultMessages.SuccessListed);
    }


    public async Task<IDataResult<ReservationDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var entity = await _repository.GetAll()
            .Include(r => r.Table)
            .Include(r => r.Customer)
            .Where(r => r.Id == id).FirstOrDefaultAsync();

        if (entity is null)
        {
            return new ErrorDataResult<ReservationDetailResponseDto>(ResultMessages.NotFound);
        }

        var dto = _mapper.Map<ReservationDetailResponseDto>(entity);
        return new SuccessDataResult<ReservationDetailResponseDto>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<ReservationDetailResponseDto>>> GetDetailAllAsync()
    {
        var reservations = await _repository.GetAll(r => !r.IsDeleted).Include(r => r.Customer).Include(r => r.Table).ToListAsync();

        var dto = _mapper.Map<IEnumerable<ReservationDetailResponseDto>>(reservations);

        return new SuccessDataResult<IEnumerable<ReservationDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<ReservationDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var reservations = await _repository.GetAll(r => r.IsDeleted).Include(r => r.Customer).Include(r => r.Table).ToListAsync();

        var dto = _mapper.Map<IEnumerable<ReservationDetailResponseDto>>(reservations);

        return new SuccessDataResult<IEnumerable<ReservationDetailResponseDto>>(dto, ResultMessages.SuccessListed);
    }
    public async Task<IDataResult<List<ReservationResponseDto>>> GetReservationsByTableIdAsync(int tableId)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var list = await _repository.GetAll(r =>
                r.TableId == tableId &&
                r.IsActive && !r.IsDeleted &&
                r.IsConfirm &&
                r.StartAt >= today && r.StartAt < tomorrow)
            .AsNoTracking()
            .OrderBy(r => r.StartAt)
            .Select(r => new ReservationResponseDto
            {
                Id = r.Id,
                StartAt = r.StartAt,
                EndAt = r.EndAt,
                GuestCount = r.GuestCount
                // istersen Customer bilgisi de ekle
            })
            .ToListAsync();

        return new SuccessDataResult<List<ReservationResponseDto>>(list, ResultMessages.SuccessListed);
    }

}