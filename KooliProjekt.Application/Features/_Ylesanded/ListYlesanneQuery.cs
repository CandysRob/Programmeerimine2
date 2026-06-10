using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features._Ylesanded
{
    [ExcludeFromCodeCoverage]
    public class ListYlesanneQuery : IRequest<OperationResult<PagedResult<Ylesanne>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int MaxPageSize { get; set; } = 50;

        // Search parameters
        public string? Pealkiri { get; set; }
    }
}
