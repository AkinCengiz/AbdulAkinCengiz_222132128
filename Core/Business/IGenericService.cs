using Core.Entity;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business;
public interface IGenericService<TEntity, TResponse, in TCreate, in TUpdate, TDetail>
    where TEntity : class,IEntity, new()
    where TResponse : class, IResponseDto
    where TCreate : class, ICreateDto
    where TUpdate : class, IUpdateDto
    where TDetail : class, IDetailDto
{
    Task<IDataResult<TResponse>> AddAsync(TCreate dto);
    Task<IResult> UpdateAsync(TUpdate dto);
    Task<IResult> RemoveAsync(int id);
    Task<IDataResult<TResponse>> GetByIdAsync(int id);
    Task<IDataResult<IEnumerable<TResponse>>> GetAllAsync();
    Task<IDataResult<IEnumerable<TResponse>>> GetAllDeletedAsync();
    Task<IDataResult<TDetail>> GetDetailByIdAsync(int id);
    Task<IDataResult<IEnumerable<TDetail>>> GetDetailAllAsync();
    Task<IDataResult<IEnumerable<TDetail>>> GetDetailAllDeletedAsync();
}