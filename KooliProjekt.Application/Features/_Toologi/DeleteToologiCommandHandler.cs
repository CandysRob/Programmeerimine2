using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Toologi
{
    public class DeleteToologiCommandHandler : IRequestHandler<DeleteToologiCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteToologiCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteToologiCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Toologid
                .Where(t => t.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return result;
        }
    }
}
