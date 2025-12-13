using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Order;
using AutoMapper;
using Core.Business;
using Core.DataAccess;
using Core.UnitOfWorks;
using Core.Utilities.Results;
using FluentValidation;

namespace AbdulAkinCengiz_222132128.Business.Concrete;
public sealed class OrderManager : IOrderService
{
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

    public Task<IDataResult<OrderResponseDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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
}
