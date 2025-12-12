using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class GetYlesanneQueryHandler : IRequestHandler<GetYlesanneQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetYlesanneQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetYlesanneQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Ylesanded
                .Include(y => y.Projekt)
                .Include(y => y.Tootaja)
                .Where(y => y.Id == request.Id)
                .Select(y => new
                {
                    y.Id,
                    y.Pealkiri,
                    y.Kirjeldus,
                    y.Tahtaeg,
                    y.Staatus,
                    y.TunnidKokku,
                    Projekt = new
                    {
                        y.Projekt.Id,
                        y.Projekt.Nimi
                    },
                    Tootaja = new
                    {
                        y.Tootaja.Id,
                        y.Tootaja.Nimi
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
