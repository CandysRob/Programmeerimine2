using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Projektid
{
    public class GetProjektQueryHandler : IRequestHandler<GetProjektQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjektQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetProjektQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Projektid
                .Where(p => p.Id == request.Id)
                .Select(p => new
                {
                    p.Id,
                    p.Nimi,
                    p.Kirjeldus,
                    p.Alguskuupaev,
                    p.Lopetatuskuupaev
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
