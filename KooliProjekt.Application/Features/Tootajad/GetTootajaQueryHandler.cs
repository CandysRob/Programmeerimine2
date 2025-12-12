using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Tootajad
{
    public class GetTootajaQueryHandler : IRequestHandler<GetTootajaQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTootajaQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetTootajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .Tootajad
                .Where(t => t.Id == request.Id)
                .Select(t => new
                {
                    t.Id,
                    t.Nimi,
                    t.Email,
                    t.Ametikoht
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
