using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Tootajad
{
    public class SaveTootajaCommandHandler : IRequestHandler<SaveTootajaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveTootajaCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveTootajaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var tootaja = new Tootaja();
            if (request.Id == 0)
            {
                await _dbContext.Tootajad.AddAsync(tootaja, cancellationToken);
            }
            else
            {
                tootaja = await _dbContext.Tootajad.FindAsync(new object[] { request.Id }, cancellationToken);
            }

            tootaja.Nimi = request.Nimi;
            tootaja.Email = request.Email;
            tootaja.Ametikoht = request.Ametikoht;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
