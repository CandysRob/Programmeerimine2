using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projektid
{
    public class SaveProjektCommandHandler : IRequestHandler<SaveProjektCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjektCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjektCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var projekt = new Projekt();
            if (request.Id == 0)
            {
                await _dbContext.Projektid.AddAsync(projekt, cancellationToken);
            }
            else
            {
                projekt = await _dbContext.Projektid.FindAsync(new object[] { request.Id }, cancellationToken);
            }

            projekt.Nimi = request.Nimi;
            projekt.Kirjeldus = request.Kirjeldus;
            projekt.Alguskuupaev = request.Alguskuupaev;
            projekt.Lopetatuskuupaev = request.Lopetatuskuupaev;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
