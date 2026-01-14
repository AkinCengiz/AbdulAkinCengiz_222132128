using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AutoMapper;
using Core.Business.Constants;
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
    private readonly IProductDal _productDal;

    public OrderManager(IOrderDal orderDal, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderCreateRequestDto> createValidator, IValidator<OrderUpdateRequestDto> updateValidator, IOrderItemDal orderItemDal, IProductDal productDal)
    {
        _orderDal = orderDal;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _orderItemDal = orderItemDal;
        _productDal = productDal;
    }

    public async Task<IDataResult<OrderResponseDto>> AddAsync(OrderCreateRequestDto dto)
    {
        var isValid = await _createValidator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var errors = isValid.Errors
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToList();
            return new ErrorDataResult<OrderResponseDto>(String.Join(" | ",errors));
        }


        var exists = await _orderDal.AnyAsync(o =>
            o.ReservationId == dto.ReservationId &&
            !o.IsDeleted &&
            !o.IsPaid &&
            o.IsActive);

        if (exists)
            return new ErrorDataResult<OrderResponseDto>("Bu rezervasyona ait bir sipariş zaten mevcut.");
        var order = _mapper.Map<Order>(dto);
        await _orderDal.AddAsync(order);
        await _unitOfWork.CommitAsync();
        var responseDto = _mapper.Map<OrderResponseDto>(order);
        return new SuccessDataResult<OrderResponseDto>(responseDto, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(OrderUpdateRequestDto dto)
    {
        var isValid = await _updateValidator.ValidateAsync(dto);
        if (!isValid.IsValid)
        {
            var errors = isValid.Errors
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToList();
            return new ErrorResult(String.Join(" | ", errors));
        }

        var order = await _orderDal.GetByIdAsync(dto.Id);
        if (order == null)
            return new ErrorResult("Sipariş bulunamadı.");
        _mapper.Map(dto, order);
        _orderDal.Update(order);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _orderDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorResult("Sipariş bulunamadı.");
        _orderDal.SoftDelete(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await _orderDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorResult("Sipariş bulunamadı.");
        _orderDal.Remove(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<OrderResponseDto>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return new ErrorDataResult<OrderResponseDto>("Geçersiz sipariş.");

        var order = await _orderDal.GetAll(o => o.Id == id && !o.IsDeleted)
            .Include(o => o.Reservation)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.Category)
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

    public async Task<IDataResult<IEnumerable<OrderResponseDto>>> GetAllAsync()
    {
        var entities = await _orderDal.GetAll(o => !o.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<OrderResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderResponseDto>>(dtos, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderResponseDto>>> GetAllDeletedAsync()
    {
        var entities = await _orderDal.GetAll(o => o.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<OrderResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderResponseDto>>(dtos, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<OrderDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var entity = await _orderDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorDataResult<OrderDetailResponseDto>("Sipariş bulunamadı.");
        var dto = _mapper.Map<OrderDetailResponseDto>(entity);
        return new SuccessDataResult<OrderDetailResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderDetailResponseDto>>> GetDetailAllAsync()
    {
        var entities = await _orderDal.GetAll(o => !o.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<OrderDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderDetailResponseDto>>(dtos, ResultMessages.SuccessListed);
    }

    public async Task<IDataResult<IEnumerable<OrderDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var entities = await _orderDal.GetAll(o => o.IsDeleted).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<OrderDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderDetailResponseDto>>(dtos, ResultMessages.SuccessListed);
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

        // 1) Incoming normalize + ProductId bazlı birleştir
        var incoming = (items ?? Enumerable.Empty<OrderItemCreateRequestDto>())
            .Where(x => x.ProductId > 0 && x.Quantity > 0)
            .GroupBy(x => x.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                QuantityInt = g.Sum(x => x.Quantity)
            })
            .ToList();

        if (incoming.Count == 0)
            return new ErrorResult("Sipariş kalemi yok.");

        // Quantity byte ise taşma kontrolü
        if (incoming.Any(x => x.QuantityInt > byte.MaxValue))
            return new ErrorResult($"Adet 1 ürün için en fazla {byte.MaxValue} olabilir.");

        // 2) Order kontrolü
        var order = await _orderDal.GetAsync(o => o.Id == orderId && !o.IsDeleted);
        if (order is null)
            return new ErrorResult("Sipariş bulunamadı.");

        if (order.IsPaid)
            return new ErrorResult("Ödenmiş sipariş güncellenemez.");

        // 3) Ürünleri tek sorguda çek (fiyat DB’den)
        var productIds = incoming.Select(x => x.ProductId).Distinct().ToList();

        var products = await _productDal
            .GetAll(p => productIds.Contains(p.Id) && !p.IsDeleted && p.IsActive)
            .Select(p => new
            {
                p.Id,
                p.Price,
                Stock = (int?)p.Stock // Stock int ise de sorun olmaz; nullable güvenliği
            })
            .ToListAsync();

        var foundIds = products.Select(x => x.Id).ToHashSet();
        var missingIds = productIds.Where(id => !foundIds.Contains(id)).ToList();
        if (missingIds.Count > 0)
            return new ErrorResult("Bazı ürünler bulunamadı veya pasif: " + string.Join(",", missingIds));

        var priceMap = products.ToDictionary(x => x.Id, x => x.Price);
        var stockMap = products.ToDictionary(x => x.Id, x => x.Stock);

        // 3.1) Stok kontrolü (stok düşürmüyor; sadece doğruluyor)
        foreach (var inc in incoming)
        {
            var stock = stockMap[inc.ProductId] ?? 0; // null ise 0 kabul
            if (stock < inc.QuantityInt)
                return new ErrorResult($"Stok yetersiz. ProductId={inc.ProductId}");
        }

        // 4) Mevcut item’ları deleted dahil çek (revive/temizlik için)
        var existing = await _orderItemDal
            .GetAll(x => x.OrderId == orderId)
            .ToListAsync();

        // 4.1) Aynı ProductId’den birden fazla AKTİF satır varsa temizlik:
        foreach (var grp in existing.Where(x => !x.IsDeleted).GroupBy(x => x.ProductId))
        {
            var keep = grp.OrderByDescending(x => x.Id).First();
            foreach (var extra in grp.Where(x => x.Id != keep.Id))
            {
                extra.IsDeleted = true;
                extra.IsActive = false;
                extra.UpdateAt = DateTime.Now;
                _orderItemDal.Update(extra);
            }
        }

        // (Aktif varsa onu; yoksa en güncel deleted olanı seç)
        var existingByProductId = existing
            .GroupBy(x => x.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.Where(x => !x.IsDeleted).OrderByDescending(x => x.Id).FirstOrDefault()
                     ?? g.OrderByDescending(x => x.Id).First()
            );

        var incomingSet = incoming.Select(x => x.ProductId).ToHashSet();

        // 5) Incoming’de olmayan AKTİF kalemleri soft delete
        foreach (var ex in existing.Where(e => !e.IsDeleted))
        {
            if (!incomingSet.Contains(ex.ProductId))
            {
                ex.IsDeleted = true;
                ex.IsActive = false;
                ex.UpdateAt = DateTime.Now;
                _orderItemDal.Update(ex);
            }
        }

        // 6) Upsert
        foreach (var inc in incoming)
        {
            var qty = (byte)inc.QuantityInt; // Quantity int ise cast kaldır

            // fiyat mutlaka DB’den
            if (!priceMap.TryGetValue(inc.ProductId, out var dbPrice))
                return new ErrorResult($"Ürün fiyatı bulunamadı. ProductId={inc.ProductId}");

            if (existingByProductId.TryGetValue(inc.ProductId, out var ex))
            {
                // varsa update: sadece Quantity güncelle (fiyat SABİT kalsın)
                ex.Quantity = qty;

                // revive
                ex.IsActive = true;
                ex.IsDeleted = false;
                ex.UpdateAt = DateTime.Now;

                // Edge-case: eski kayıtta UnitPrice 0 kalmışsa ilk seferde set edebilirsin
                // if (ex.UnitPrice <= 0) ex.UnitPrice = dbPrice;

                _orderItemDal.Update(ex);
            }
            else
            {
                // yoksa insert: DB fiyatını yaz
                var entity = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = inc.ProductId,
                    Quantity = qty,
                    UnitPrice = dbPrice,
                    IsActive = true,
                    IsDeleted = false,
                    CreateAt = DateTime.Now
                };

                await _orderItemDal.AddAsync(entity);
            }
        }

        await _unitOfWork.CommitAsync();
        return new SuccessResult("Sipariş kalemleri kaydedildi.");
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
