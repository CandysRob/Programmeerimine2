using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Ylesanded
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

            var list = new Ylesanne();
            if (request.Id == 0)
            {
                await _dbContext.Ylesanded.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Ylesanded.FindAsync(request.Id);
                //_dbContext.ToDoLists.Update(list);
            }

            list.Pealkiri = request.Pealkiri;
            list.Kirjeldus = request.Kirjeldus;
            list.Tahtaeg = request.Tahtaeg;
            list.Staatus = request.Staatus;
            list.TunnidKokku = request.TunnidKokku;
            list.ProjektId = request.ProjektId;
            list.TootajaId = request.TootajaId;

            await _dbContext.SaveChangesAsync();

            return result;
            
        }
    }
}
