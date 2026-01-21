using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features._Projektid
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

            var list = new Projekt();
            if (request.Id == 0)
            {
                await _dbContext.Projektid.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Projektid.FindAsync(request.Id);
                //_dbContext.ToDoLists.Update(list);
            }

            list.Nimi = request.Nimi;
            list.Kirjeldus = request.Kirjeldus;
            list.Alguskuupaev = request.Alguskuupaev;
            list.Lopetatuskuupaev = request.Lopetatuskuupaev;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
