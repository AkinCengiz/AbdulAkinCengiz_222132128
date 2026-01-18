using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Customer;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Payment;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AbdulAkinCengiz_222132128.Entity.Dtos.Reservation;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
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
    private readonly IReservationDal _reservationDal;
    private const decimal VatRate = 0.08m;

    public OrderManager(IOrderDal orderDal, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderCreateRequestDto> createValidator, IValidator<OrderUpdateRequestDto> updateValidator, IOrderItemDal orderItemDal, IProductDal productDal, IReservationDal reservationDal)
    {
        _orderDal = orderDal;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _orderItemDal = orderItemDal;
        _productDal = productDal;
        _reservationDal = reservationDal;
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
            .Include(o => o.Reservation).ThenInclude(r => r.Table)
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                    .ThenInclude(p => p.Category)
            .AsNoTracking()
            .Select(o => new OrderResponseDto
            {
                Id = o.Id,
                ReservationId = o.ReservationId,
                IsPaid = o.IsPaid,

                Total = o.Total,

                OrderItems = o.OrderItems
                    .Where(i => !i.IsDeleted)
                    .Select(i => new OrderItemResponseDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Product = new ProductResponseDto
                        {
                            Id = i.Product.Id,
                            Name = i.Product.Name,
                            Price = i.Product.Price,
                            Stock = i.Product.Stock,
                            Category = new CategoryResponseDto
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

        // ✅ ekstra güvenlik: OrderItems null ise boş liste yap
        order.OrderItems ??= new List<OrderItemResponseDto>();

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

        var now = DateTime.Now;

        // 1) varsa getir
        var existingId = await _orderDal.GetAll(o =>
                o.ReservationId == reservationId &&
                o.IsActive && !o.IsDeleted && !o.IsPaid)
            .Select(o => (int?)o.Id)
            .FirstOrDefaultAsync();

        if (existingId.HasValue)
        {
            // ✅ Reservation.OrderId mirror
            var reservation = await _reservationDal.GetAsync(r =>
                r.Id == reservationId && !r.IsDeleted && r.IsActive);

            if (reservation != null && reservation.OrderId != existingId.Value)
            {
                reservation.OrderId = existingId.Value;
                reservation.UpdateAt = now;
                _reservationDal.Update(reservation);
                await _unitOfWork.CommitAsync();
            }

            return new SuccessDataResult<int>(existingId.Value, "Aktif sipariş bulundu.");
        }

        // 2) yoksa oluştur
        var order = new Order
        {
            ReservationId = reservationId,
            IsActive = true,
            IsDeleted = false,
            IsPaid = false,
            CreateAt = now
        };

        await _orderDal.AddAsync(order);
        await _unitOfWork.CommitAsync(); // order.Id oluşsun

        // ✅ Reservation.OrderId mirror
        var res2 = await _reservationDal.GetAsync(r =>
            r.Id == reservationId && !r.IsDeleted && r.IsActive);

        if (res2 != null && res2.OrderId != order.Id)
        {
            res2.OrderId = order.Id;
            res2.UpdateAt = now;
            _reservationDal.Update(res2);
            await _unitOfWork.CommitAsync();
        }

        return new SuccessDataResult<int>(order.Id, "Sipariş oluşturuldu.");
    }



    public async Task<IDataResult<decimal>> SaveItemsAsync(int orderId, IEnumerable<OrderItemCreateRequestDto> items)
    {
        if (orderId <= 0)
            return new ErrorDataResult<decimal>("Geçersiz sipariş.");

        // 1) Incoming normalize + ProductId bazlı birleştir (UI kaynağımız)
        var incoming = (items ?? Enumerable.Empty<OrderItemCreateRequestDto>())
            .Where(x => x.ProductId > 0 && x.Quantity > 0)
            .GroupBy(x => x.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                QuantityInt = g.Sum(x => (int)x.Quantity)
            })
            .ToList();

        if (incoming.Count == 0)
            return new ErrorDataResult<decimal>("Sipariş kalemi yok.");

        if (incoming.Any(x => x.QuantityInt > byte.MaxValue))
            return new ErrorDataResult<decimal>($"Adet 1 ürün için en fazla {byte.MaxValue} olabilir.");

        // 2) Order kontrolü
        var order = await _orderDal.GetAsync(o => o.Id == orderId && !o.IsDeleted && o.IsActive);
        if (order is null)
            return new ErrorDataResult<decimal>("Sipariş bulunamadı.");

        if (order.IsPaid)
            return new ErrorDataResult<decimal>("Ödenmiş sipariş güncellenemez.");

        // 3) Mevcut kalemleri çek (aktif, silinmemiş olanlar stok senkronunda esas alınacak)
        var existingActive = await _orderItemDal
            .GetAll(x => x.OrderId == orderId && !x.IsDeleted && x.IsActive)
            .ToListAsync();

        // Aynı ProductId’den birden fazla aktif satır varsa tekilleştir (opsiyonel ama iyi)
        foreach (var grp in existingActive.GroupBy(x => x.ProductId))
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

        // Tekrar çekmeye gerek yok; keep’ler zaten listede
        existingActive = existingActive
            .Where(x => !x.IsDeleted && x.IsActive)
            .GroupBy(x => x.ProductId)
            .Select(g => g.OrderByDescending(x => x.Id).First())
            .ToList();

        var existingQtyMap = existingActive.ToDictionary(x => x.ProductId, x => (int)x.Quantity);

        var incomingQtyMap = incoming.ToDictionary(x => x.ProductId, x => x.QuantityInt);

        // 4) İşlem görecek tüm productId’ler (incoming + existing)
        var allProductIds = incomingQtyMap.Keys.Union(existingQtyMap.Keys).ToList();

        // 5) Ürünleri tek sorguda çek (fiyat + stok)
        var products = await _productDal
            .GetAll(p => allProductIds.Contains(p.Id) && !p.IsDeleted && p.IsActive)
            .ToListAsync();

        var productById = products.ToDictionary(p => p.Id, p => p);

        // Ürün doğrulama
        var missingIds = allProductIds.Where(id => !productById.ContainsKey(id)).ToList();
        if (missingIds.Count > 0)
            return new ErrorDataResult<decimal>("Bazı ürünler bulunamadı veya pasif: " + string.Join(",", missingIds));

        // 6) Stok kontrolü (net düşüş gereken ürünler)
        // net = newQty - oldQty; net>0 ise stoktan düşülecek
        foreach (var pid in allProductIds)
        {
            var newQty = incomingQtyMap.TryGetValue(pid, out var nq) ? nq : 0;
            var oldQty = existingQtyMap.TryGetValue(pid, out var oq) ? oq : 0;

            var netDecrease = newQty - oldQty;
            if (netDecrease > 0)
            {
                var pr = productById[pid];
                if (pr.Stock < netDecrease)
                    return new ErrorDataResult<decimal>($"Stok yetersiz: {pr.Name}");
            }
        }

        // 7) DB item’ları (deleted dahil) upsert için çek
        var existingAll = await _orderItemDal
            .GetAll(x => x.OrderId == orderId)
            .ToListAsync();

        // upsert lookup: ürün bazında (aktif olan varsa onu; yoksa en günceli)
        var existingByProduct = existingAll
            .GroupBy(x => x.ProductId)
            .ToDictionary(
                g => g.Key,
                g => g.Where(x => !x.IsDeleted && x.IsActive).OrderByDescending(x => x.Id).FirstOrDefault()
                     ?? g.OrderByDescending(x => x.Id).First()
            );

        // 8) Delta uygula: stok + kalem sync
        foreach (var pid in allProductIds)
        {
            var newQty = incomingQtyMap.TryGetValue(pid, out var nq) ? nq : 0;
            var oldQty = existingQtyMap.TryGetValue(pid, out var oq) ? oq : 0;

            var net = newQty - oldQty; // + ise düş, - ise iade et

            // Stok güncelle
            if (net != 0)
            {
                var pr = productById[pid];
                pr.Stock -= net; // net negatifse stok artar (iade)
                _productDal.Update(pr);
            }

            // DB kalem senkronu
            if (existingByProduct.TryGetValue(pid, out var ex))
            {
                if (newQty <= 0)
                {
                    // UI’da yok -> soft delete
                    ex.IsDeleted = true;
                    ex.IsActive = false;
                    ex.UpdateAt = DateTime.Now;
                    _orderItemDal.Update(ex);
                }
                else
                {
                    // UI’da var -> update
                    ex.Quantity = (byte)newQty;

                    // UnitPrice politikanız:
                    // - İlk eklemede DB fiyatı yazıyorsunuz (doğru)
                    // - Sonradan fiyat değişse bile sipariş kalem fiyatını sabit tutmak istiyorsanız ex.UnitPrice'a dokunmayın.
                    // - Eğer güncel fiyatla güncellensin istiyorsanız: ex.UnitPrice = productById[pid].Price;

                    ex.IsDeleted = false;
                    ex.IsActive = true;
                    ex.UpdateAt = DateTime.Now;
                    _orderItemDal.Update(ex);
                }
            }
            else
            {
                // DB’de yok, UI’da varsa -> insert
                if (newQty > 0)
                {
                    var pr = productById[pid];
                    var entity = new OrderItem
                    {
                        OrderId = orderId,
                        ProductId = pid,
                        Quantity = (byte)newQty,
                        UnitPrice = pr.Price, // sipariş anındaki fiyatı sabitle
                        IsActive = true,
                        IsDeleted = false,
                        CreateAt = DateTime.Now
                    };
                    await _orderItemDal.AddAsync(entity);
                }
            }
        }

        await _unitOfWork.CommitAsync();
        var subTotal = await _orderItemDal.GetAll(x =>
                x.OrderId == orderId &&
                !x.IsDeleted &&
                x.IsActive)
            .SumAsync(x => x.UnitPrice * x.Quantity);

        // 2️⃣ KDV
        var vatAmount = Math.Round(
            subTotal * VatRate,
            2,
            MidpointRounding.AwayFromZero
        );

        // 3️⃣ Genel toplam (KDV dahil)
        var grandTotal = subTotal + vatAmount;

        // 4️⃣ Order tablosunu güncelle
        order.Total = grandTotal;        // veya GrandTotal
        order.UpdateAt = DateTime.Now;

        _orderDal.Update(order);
        await _unitOfWork.CommitAsync();

        // 5️⃣ KDV DAHİL TOTAL DÖN
        return new SuccessDataResult<decimal>(
            grandTotal,
            $"Sipariş kaydedildi. (Ara: {subTotal:N2}, KDV: {vatAmount:N2})"
        );

        // order.ReservationId boş değilse Reservation.OrderId'yi garanti doldur
        if (order.ReservationId <= 0)
            return new ErrorDataResult<decimal>("Sipariş rezervasyona bağlı değil.");

        var reservation = await _reservationDal.GetAsync(r =>
            r.Id == order.ReservationId && !r.IsDeleted && r.IsActive);

        if (reservation != null && reservation.OrderId != order.Id)
        {
            reservation.OrderId = order.Id;
            reservation.UpdateAt = DateTime.Now;
            _reservationDal.Update(reservation);
            await _unitOfWork.CommitAsync();
        }

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


    public async Task<IResult> PayAsync(int orderId)
    {
        if (orderId <= 0)
            return new ErrorResult("Geçersiz sipariş.");

        var now = DateTime.Now;

        var order = await _orderDal.GetAll(o => o.Id == orderId && !o.IsDeleted)
            .Include(o => o.Reservation)
            .FirstOrDefaultAsync();

        if (order == null)
            return new ErrorResult("Sipariş bulunamadı.");

        if (order.IsPaid)
            return new ErrorResult("Bu sipariş zaten ödenmiş.");

        // 1️⃣ Order güncelle
        order.IsPaid = true;
        order.IsActive = false;
        order.UpdateAt = now;
        _orderDal.Update(order);

        // 2️⃣ Reservation güncelle
        if (order.Reservation != null)
        {
            order.Reservation.IsActive = false;
            order.Reservation.IsCheckedIn = false;
            order.Reservation.UpdateAt = now;
            _reservationDal.Update(order.Reservation);
        }

        await _unitOfWork.CommitAsync();

        return new SuccessResult("Ödeme başarıyla alındı.");
    }
    public async Task<IDataResult<OrderPaymentResponseDto>> GetPaymentContextByOrderIdAsync(int orderId)
    {
        if (orderId <= 0)
            return new ErrorDataResult<OrderPaymentResponseDto>("Geçersiz sipariş.");

        var ctx = await _orderDal.GetAll(o => o.Id == orderId && !o.IsDeleted)
            .Include(o => o.Reservation).ThenInclude(r => r.Table)
            .Include(o => o.Reservation).ThenInclude(r => r.Customer)
            .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
            // Eğer Payment tablosu Order’a bağlıysa Include ekleyin:
            //.Include(o => o.Payments)
            .AsNoTracking()
            .Select(o => new OrderPaymentResponseDto
            {
                OrderId = o.Id,
                Total = o.Total,
                IsPaid = o.IsPaid,
                IsActive = o.IsActive,

                ReservationId = o.ReservationId,
                TableName = o.Reservation.Table.Name,
                CustomerFullName = o.Reservation.Customer.FirstName + " " + o.Reservation.Customer.LastName,
                StartAt = o.Reservation.StartAt,
                EndAt = o.Reservation.EndAt,

                Items = o.OrderItems
                    .Where(i => !i.IsDeleted && i.IsActive)
                    .Select(i => new OrderItemPaymentRowDto()
                    {
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList(),

                // Payment tablonuz varsa burayı kendi yapınıza göre doldurun
                Payments = new List<PaymentRowDto>()
            })
            .FirstOrDefaultAsync();

        if (ctx is null)
            return new ErrorDataResult<OrderPaymentResponseDto>("Sipariş bulunamadı.");

        return new SuccessDataResult<OrderPaymentResponseDto>(ctx, "Sipariş bilgileri getirildi.");
    }

}
