using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.toologi
{
    public class SaveToologiCommandHandler : IRequestHandler<SaveToologiCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveToologiCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveToologiCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var toologi = new toologi();
            if (request.Id == 0)
            {
                await _dbContext.Toologid.AddAsync(toologi, cancellationToken);
            }
            else
            {
                toologi = await _dbContext.Toologid.FindAsync(new object[] { request.Id }, cancellationToken);
            }

            toologi.Nimi = request.Nimi;
            toologi.starttime = request.starttime;
            toologi.endtime = request.endtime;
            toologi.Kirjeldus = request.Kirjeldus;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
