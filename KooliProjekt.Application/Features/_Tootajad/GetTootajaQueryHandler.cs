using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features._Tootajad
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
                .Toologid
                .Where(list => list.Id == request.Id)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
