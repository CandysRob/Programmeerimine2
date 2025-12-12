using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class SaveYlesanneCommandHandler : IRequestHandler<SaveYlesanneCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveYlesanneCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveYlesanneCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var ylesanne = new Ylesanne();
            if (request.Id == 0)
            {
                await _dbContext.Ylesanded.AddAsync(ylesanne, cancellationToken);
            }
            else
            {
                ylesanne = await _dbContext.Ylesanded.FindAsync(new object[] { request.Id }, cancellationToken);
            }

            ylesanne.Pealkiri = request.Pealkiri;
            ylesanne.Kirjeldus = request.Kirjeldus;
            ylesanne.Tahtaeg = request.Tahtaeg;
            ylesanne.Staatus = request.Staatus;
            ylesanne.TunnidKokku = request.TunnidKokku;
            ylesanne.ProjektId = request.ProjektId;
            ylesanne.TootajaId = request.TootajaId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
