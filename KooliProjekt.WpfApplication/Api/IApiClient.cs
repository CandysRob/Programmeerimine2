using System;
using System.Collections.Generic;
using System.Text;

namespace KooliProjekt.WpfApplication
{
    public interface IApiClient
    {
        Task<OperationResult<PagedResult<toologi>>> List(int page, int pageSize);
        Task<OperationResult> Save(toologi toolog);
        Task<OperationResult> Delete(int id);
    }
}