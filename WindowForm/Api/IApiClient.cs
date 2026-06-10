using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowForm.Api
{
    public interface IApiClient
    {
        Task<OperationResult<PagedResult<Arve>>> List(int page, int pageSize);
        Task<OperationResult> Save(Arve item);
        Task<OperationResult> Delete(int id);
    }
}