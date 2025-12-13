using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.OrderItem;
using AutoMapper;
using Core.Business;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class OrderItemManager : IOrderItemService
{
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

    public Task<IDataResult<OrderItemResponseDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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
}
