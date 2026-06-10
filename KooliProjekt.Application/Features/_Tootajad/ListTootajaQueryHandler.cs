using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.Infrastructure.Paging;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class ListTootajaQueryHandler : IRequestHandler<ListTootajaQuery, OperationResult<PagedResult<Tootaja>>>
    {
        private readonly ApplicationDbContext _db_context;

        public ListTootajaQueryHandler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult<PagedResult<Tootaja>>> Handle(ListTootajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Tootaja>>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0 || request.PageSize <= 0 || request.PageSize > request.MaxPageSize)
            {
                throw new ArgumentException(nameof(request));
            }

            var query = _db_context.Tootajad.AsQueryable();

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
