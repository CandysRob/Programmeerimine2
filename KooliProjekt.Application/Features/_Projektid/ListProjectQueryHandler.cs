using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.Infrastructure.Paging;

namespace KooliProjekt.Application.Features._Projektid
{
    public class ListProjectQueryHandler : IRequestHandler<ListProjektQuery, OperationResult<PagedResult<Projekt>>>
    {
        private readonly ApplicationDbContext _db_context;

        public ListProjectQueryHandler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult<PagedResult<Projekt>>> Handle(ListProjektQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Projekt>>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0 || request.PageSize <= 0 || request.PageSize > request.MaxPageSize)
            {
                throw new ArgumentException(nameof(request));
            }

            var query = _db_context.Projektid.AsQueryable();

            if (!string.IsNullOrEmpty(request.Nimi))
            {
                query = query.Where(list => list.Nimi.Contains(request.Nimi));
            }

            result.Value = await query
                .OrderBy(list => list.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
