using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;
using AutoMapper;
using Core.Business.Constants;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class OrderItemManager : IOrderItemService
{
    private readonly IOrderItemDal _orderItemDal;
    private readonly IOrderDal _orderDal;
    private readonly IProductDal _productDal;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderItemCreateRequestDto> _createValidator;
    private readonly IValidator<OrderItemUpdateRequestDto> _updateValidator;

    public OrderItemManager(IOrderItemDal orderItemDal, IOrderDal orderDal, IProductDal productDal, IUnitOfWork unitOfWork, IMapper mapper, IValidator<OrderItemCreateRequestDto> createValidator, IValidator<OrderItemUpdateRequestDto> updateValidator)
    {
        _orderItemDal = orderItemDal;
        _orderDal = orderDal;
        _productDal = productDal;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<IDataResult<OrderItemResponseDto>> AddAsync(OrderItemCreateRequestDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);
        var entity = _mapper.Map<OrderItem>(dto);
        await _orderItemDal.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        var responseDto = _mapper.Map<OrderItemResponseDto>(entity);
        return new SuccessDataResult<OrderItemResponseDto>(responseDto, ResultMessages.SuccessCreated);
    }

    public async Task<IResult> UpdateAsync(OrderItemUpdateRequestDto dto)
    {
        
        var entity = await _orderItemDal.GetByIdAsync(dto.Id);
        if (entity == null)
            return new ErrorResult("Sipariş kalemi bulunamadı.");

        await _updateValidator.ValidateAndThrowAsync(dto);

        _mapper.Map(dto, entity);
        _orderItemDal.Update(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessUpdated);
    }

    public async Task<IResult> RemoveAsync(int id)
    {
        var entity = await _orderItemDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorResult("Sipariş kalemi bulunamadı.");
        _orderItemDal.SoftDelete(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var entity = await _orderItemDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorResult("Sipariş kalemi bulunamadı.");
        _orderItemDal.Remove(entity);
        await _unitOfWork.CommitAsync();
        return new SuccessResult(ResultMessages.SuccessDeleted);
    }

    public async Task<IDataResult<OrderItemResponseDto>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return new ErrorDataResult<OrderItemResponseDto>("Geçersiz OrderItem Id");

        var entity = await _orderItemDal.GetAll()
            .Include(x => x.Product)
            .Where(x => x.Id == id && !x.IsDeleted)
            .Select(x => new OrderItemResponseDto
            {
                Id = x.Id,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Product = new ProductResponseDto()
                {
                    Id = x.Product.Id,
                    Category = new CategoryResponseDto()
                    {
                        Id = x.Product.Category.Id,
                        Name = x.Product.Category.Name
                    },
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Stock = x.Product.Stock
                },
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                LineTotal = x.UnitPrice * x.Quantity
            })
            .FirstOrDefaultAsync();

        if (entity == null)
            return new ErrorDataResult<OrderItemResponseDto>("Sipariş kalemi bulunamadı.");

        return new SuccessDataResult<OrderItemResponseDto>(entity, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderItemResponseDto>>> GetAllAsync()
    {
        var entity = _orderItemDal.GetAll(o => !o.IsDeleted).OrderBy(o => o.OrderId).ToListAsync();
        var dto = _mapper.Map<IEnumerable<OrderItemResponseDto>>(entity);
        return new SuccessDataResult<IEnumerable<OrderItemResponseDto>>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderItemResponseDto>>> GetAllDeletedAsync()
    {
        var entity = await _orderItemDal.GetAll(oi => !oi.IsDeleted).OrderBy(oi => oi.OrderId).ToListAsync();
        var dto = _mapper.Map<IEnumerable<OrderItemResponseDto>>(entity);
        return new SuccessDataResult<IEnumerable<OrderItemResponseDto>>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<OrderItemDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        var entity = await _orderItemDal.GetByIdAsync(id);
        if (entity == null)
            return new ErrorDataResult<OrderItemDetailResponseDto>("Sipariş kalemi bulunamadı.");
        var dto = _mapper.Map<OrderItemDetailResponseDto>(entity);
        return new SuccessDataResult<OrderItemDetailResponseDto>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderItemDetailResponseDto>>> GetDetailAllAsync()
    {
        var entities = await _orderItemDal.GetAll(oi => !oi.IsDeleted).OrderBy(oi => oi.OrderId).ToListAsync();
        var dto = _mapper.Map<IEnumerable<OrderItemDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderItemDetailResponseDto>>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IDataResult<IEnumerable<OrderItemDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        var entities = await _orderItemDal.GetAll(oi => oi.IsDeleted).OrderBy(oi => oi.OrderId).ToListAsync();
        var dto = _mapper.Map<IEnumerable<OrderItemDetailResponseDto>>(entities);
        return new SuccessDataResult<IEnumerable<OrderItemDetailResponseDto>>(dto, ResultMessages.SuccessGet);
    }

    public async Task<IResult> AddOrIncreaseAsync(int orderId, int productId, byte quantity)
    {
        if (orderId <= 0 || productId <= 0 || quantity <= 0)
            return new ErrorResult("Geçersiz parametre.");

        // Order aktif mi / ödenmiş mi kontrolü önerilir
        var order = await _orderDal.GetAsync(o => o.Id == orderId && !o.IsDeleted && o.IsActive && !o.IsPaid);
        if (order is null)
            return new ErrorResult("Aktif sipariş bulunamadı.");

        // Ürün var mı + stok kontrolü (varsa)
        var product = await _productDal.GetAsync(p => p.Id == productId && !p.IsDeleted && p.IsActive);
        if (product is null)
            return new ErrorResult("Ürün bulunamadı.");

        if (product.Stock < quantity)
            return new ErrorResult("Yetersiz stok.");

        // Kalem var mı?
        var item = await _orderItemDal.GetAsync(x => x.OrderId == orderId && x.ProductId == productId && !x.IsDeleted);

        if (item is null)
        {
            item = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                UnitPrice = product.Price, // sipariş anındaki fiyatı sabitlemek için
                IsActive = true,
                IsDeleted = false,
                CreateAt = DateTime.Now
            };
            await _orderItemDal.AddAsync(item);
        }
        else
        {
            item.Quantity += quantity;
            item.UpdateAt = DateTime.Now;
            _orderItemDal.Update(item);
        }

        // stok düş
        product.Stock -= quantity;
        _productDal.Update(product);

        await _unitOfWork.CommitAsync();
        return new SuccessResult("Ürün siparişe eklendi.");
    }

    public async Task<IDataResult<List<OrderItemResponseDto>>> GetOrderItemsByOrder(int orderId)
    {
        var orderItems = await _orderItemDal.GetAll(oi => !oi.IsDeleted && oi.OrderId == orderId).ToListAsync();
        var dto = _mapper.Map<List<OrderItemResponseDto>>(orderItems);
        return new SuccessDataResult<List<OrderItemResponseDto>>(dto,ResultMessages.SuccessListed);
    }
}
