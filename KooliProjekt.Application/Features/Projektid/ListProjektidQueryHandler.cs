using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projektid
{
    public class ListProjektidQueryHandler : IRequestHandler<ListProjektidQuery, OperationResult<IList<Projekt>>>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public ListProjektidQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Projekt>>> Handle(ListProjektidQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Projekt>>();
            result.Value = await _dbContext
                .Projektid
                .OrderBy(p => p.Nimi)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
