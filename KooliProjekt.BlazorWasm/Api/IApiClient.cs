using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.BlazorWasm
{
    public interface IApiClient
    {
        Task<OperationResult<toologi>> Get(int id);
        Task<OperationResult<PagedResult<toologi>>> List(int page, int pageSize);
        Task<OperationResult> Save(toologi toolog);
        Task<OperationResult> Delete(int id);
    }
}