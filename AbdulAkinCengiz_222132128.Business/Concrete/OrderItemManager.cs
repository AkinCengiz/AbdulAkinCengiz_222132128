using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.DataAccess.UnitOfWorks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
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
using AbdulAkinCengiz_222132128.Entity.Dtos.Category;
using AbdulAkinCengiz_222132128.Entity.Dtos.Product;

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

    public Task<IDataResult<OrderItemResponseDto>> AddAsync(OrderItemCreateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> UpdateAsync(OrderItemUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IResult> RemoveAsync(int id)
    {
        throw new NotImplementedException();
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

    public Task<IDataResult<IEnumerable<OrderItemResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderItemResponseDto>>> GetAllDeletedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<OrderItemDetailResponseDto>> GetDetailByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderItemDetailResponseDto>>> GetDetailAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IDataResult<IEnumerable<OrderItemDetailResponseDto>>> GetDetailAllDeletedAsync()
    {
        throw new NotImplementedException();
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

        // stok düş (istiyorsanız)
        product.Stock -= quantity;
        _productDal.Update(product);

        await _unitOfWork.CommitAsync();
        return new SuccessResult("Ürün siparişe eklendi.");
    }
}
