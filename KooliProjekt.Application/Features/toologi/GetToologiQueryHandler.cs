using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.toologi
{
    public class GetToologiQueryHandler : IRequestHandler<GetToologiQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetToologiQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetToologiQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Toologid
                .Where(t => t.Id == request.Id)
                .Select(t => new
                {
                    t.Id,
                    t.Nimi,
                    t.starttime,
                    t.endtime,
                    t.Kirjeldus
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
