using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Projektid
{
    public class DeleteProjektCommandHandler : IRequestHandler<DeleteProjektCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteProjektCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteProjektCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Projektid
                .Where(p => p.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}
