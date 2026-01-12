using AbdulAkinCengiz_222132128.Entity.Concrete;
using AbdulAkinCengiz_222132128.Entity.Dtos.Table;
using Core.Business;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbdulAkinCengiz_222132128.Business.Abstract;
public interface ITableService : IGenericService<Table,TableResponseDto,TableCreateRequestDto,TableUpdateRequestDto,TableDetailResponseDto>
{
    Task<IDataResult<List<TableCardDto>>> GetTableCardsAsync(TableStatus? statusFilter = null, int reservedThresholdMinutes = 30);
    //Task<IDataResult<TableCardDto>> GetTableDetailAsync(int tableId);
}
