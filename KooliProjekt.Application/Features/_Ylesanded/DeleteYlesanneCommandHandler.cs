using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class DeleteYlesanneCommandHandler : IRequestHandler<DeleteYlesanneCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteYlesanneCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteYlesanneCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Ylesanded
                .Where(y => y.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}
