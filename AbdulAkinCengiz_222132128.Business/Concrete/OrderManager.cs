using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AutoMapper;
using Core.Business;
using Core.Business.Constants;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class OrderManager : IOrderService
{
    private readonly IOrderDal _orderDal;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderCreateRequestDto> _createValidator;
    private readonly IValidator<OrderUpdateRequestDto> _updateValidator;
    private readonly IOrderItemDal _orderItemDal;

    public OrderManager(IOrderDal orderDal, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderCreateRequestDto> createValidator, IValidator<OrderUpdateRequestDto> updateValidator, IOrderItemDal orderItemDal)
    {
        _orderDal = orderDal;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _orderItemDal = orderItemDal;
    }

    public Task<IDataResult<OrderResponseDto>> AddAsync(OrderCreateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> UpdateAsync(OrderUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IDataResult<OrderResponseDto>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return new ErrorDataResult<OrderResponseDto>("Geçersiz sipariş.");

        var order = await _orderDal.GetAll(o => o.Id == id && !o.IsDeleted)
            .Include(o => o.Reservation)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                ReservationId = o.ReservationId,
                IsPaid = o.IsPaid,
                OrderItems = o.OrderItems
                    .Where(i => !i.IsDeleted)
                    .Select(i => new OrderItemResponseDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Product = new ProductResponseDto()
                        {
                            Id = i.Product.Id,
                            Name = i.Product.Name,
                            Price = i.Product.Price,
                            Stock = i.Product.Stock,
                            Category = new CategoryResponseDto()
                            {
                                Id = i.Product.Category.Id,
                                Name = i.Product.Category.Name
                            }
                        },
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        LineTotal = i.Quantity * i.UnitPrice
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (order == null)
            return new ErrorDataResult<OrderResponseDto>("Sipariş bulunamadı.");

        return new SuccessDataResult<OrderResponseDto>(order, ResultMessages.SuccessGet);
    }

    public Task<IDataResult<IEnumerable<OrderResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderResponseDto>>> GetAllDeletedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<OrderDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderDetailResponseDto>>> GetDetailAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<IDataResult<int>> GetOrCreateActiveOrderByReservationAsync(int reservationId)
    {
        if (reservationId <= 0)
            return new ErrorDataResult<int>("Geçersiz rezervasyon.");

        // 1) varsa getir
        var existingId = await _orderDal.GetAll(o =>
                o.ReservationId == reservationId &&
                o.IsActive && !o.IsDeleted && !o.IsPaid)
            .Select(o => (int?)o.Id)
            .FirstOrDefaultAsync();

        if (existingId.HasValue)
            return new SuccessDataResult<int>(existingId.Value, "Aktif sipariş bulundu.");

        // 2) yoksa oluştur
        var order = new Order
        {
            ReservationId = reservationId,
            IsActive = true,
            IsDeleted = false,
            IsPaid = false,
            CreateAt = DateTime.Now
        };

        await _orderDal.AddAsync(order);
        await _unitOfWork.CommitAsync();

        return new SuccessDataResult<int>(order.Id, "Sipariş oluşturuldu.");
    }

    public async Task<IResult> SaveItemsAsync(int orderId, IEnumerable<OrderItemCreateRequestDto> items)
    {
        if (orderId <= 0)
            return new ErrorResult("Geçersiz sipariş.");

        var list = items?.ToList() ?? new List<OrderItemCreateRequestDto>();
        if (list.Count == 0)
            return new ErrorResult("Sipariş kalemi yok.");

        // Order var mı?
        var order = await _orderDal.GetAsync(o => o.Id == orderId && !o.IsDeleted);
        if (order is null)
            return new ErrorResult("Sipariş bulunamadı.");

        // Mevcut item’ları çek
        var existingItems = await _orderItemDal
            .GetAll(x => x.OrderId == orderId && !x.IsDeleted)
            .ToListAsync();

        // 1) soft delete veya hard delete (senin yapına göre)
        foreach (var ex in existingItems)
        {
            ex.IsDeleted = true;
            ex.IsActive = false;
            _orderItemDal.Update(ex);
        }

        // 2) Yeni item’ları ekle
        foreach (var i in list)
        {
            if (i.ProductId <= 0 || i.Quantity <= 0) continue;

            var entity = new OrderItem
            {
                OrderId = orderId,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                IsActive = true,
                IsDeleted = false,
                CreateAt = DateTime.Now
            };

            await _orderItemDal.AddAsync(entity);
        }

        await _unitOfWork.CommitAsync();
        return new SuccessResult("Sipariş kaydedildi.");
    }

    public async Task<IDataResult<OrderContextDto>> GetContextAsync(int orderId)
    {
        if (orderId <= 0)
            return new ErrorDataResult<OrderContextDto>("Geçersiz sipariş.");

        var ctx = await _orderDal.GetAll(o => o.Id == orderId && !o.IsDeleted)
            .AsNoTracking()
            .Select(o => new OrderContextDto
            {
                OrderId = o.Id,

                // ReservationId nullable ise direkt al
                ReservationId = o.ReservationId,

                // Reservation null olabilir → TableId de null olabilir
                TableId = o.Reservation != null ? (int?)o.Reservation.TableId : null,
                TableName = o.Reservation != null && o.Reservation.Table != null
                    ? o.Reservation.Table.Name
                    : "",

                StartAt = o.Reservation != null ? (DateTime?)o.Reservation.StartAt : null,
                EndAt = o.Reservation != null ? (DateTime?)o.Reservation.EndAt : null,

                CustomerId = o.Reservation != null ? (int?)o.Reservation.CustomerId : null,
                CustomerFullName = o.Reservation != null && o.Reservation.Customer != null
                    ? (o.Reservation.Customer.FirstName + " " + o.Reservation.Customer.LastName)
                    : ""
            })
            .SingleOrDefaultAsync();

        if (ctx is null)
            return new ErrorDataResult<OrderContextDto>("Sipariş bulunamadı.");

        // Reservation bağlı değilse: buradan yönetebilirsiniz
        if (ctx.ReservationId is null || ctx.TableId is null)
            return new ErrorDataResult<OrderContextDto>("Bu sipariş bir rezervasyona bağlı değil. (ReservationId/TableId boş)");

        return new SuccessDataResult<OrderContextDto>(ctx, ResultMessages.SuccessGet);
    }



}
