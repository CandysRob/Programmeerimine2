using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tootajad
{
    public class ListTootajadQueryHandler : IRequestHandler<ListTootajadQuery, OperationResult<IList<Tootaja>>>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public ListTootajadQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IList<Tootaja>>> Handle(ListTootajadQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IList<Tootaja>>();
            result.Value = await _dbContext
                .Tootajad
                .OrderBy(t => t.Nimi)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
