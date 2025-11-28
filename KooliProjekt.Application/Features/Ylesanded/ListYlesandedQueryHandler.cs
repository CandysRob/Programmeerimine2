using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class ListYlesandedQueryHandler : IRequestHandler<ListYlesandedQuery, OperationResult<PagedResult<Ylesanne>>>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public ListYlesandedQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<Ylesanne>>> Handle(ListYlesandedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Ylesanne>>();
            
            result.Value = await _dbContext
                .Ylesanded
                .Include(y => y.Projekt)
                .Include(y => y.Tootaja)
                .OrderBy(y => y.Pealkiri)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
