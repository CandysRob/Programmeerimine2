using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
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

            var list = new Tootaja();
            if (request.Id == 0)
            {
                await _dbContext.Tootajad.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Tootajad.FindAsync(request.Id);
                //_dbContext.ToDoLists.Update(list);
            }

            list.Nimi = request.Nimi;
            list.Email = request.Email;
            list.Ametikoht = request.Ametikoht;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
