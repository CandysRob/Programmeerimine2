using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Toologi
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

            var list = new toologi();
            if (request.Id == 0)
            {
                await _dbContext.Toologid.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Toologid.FindAsync(request.Id);
                //_dbContext.ToDoLists.Update(list);
            }

            list.Nimi = request.Nimi;
            list.starttime = request.starttime;
            list.endtime = request.endtime;
            list.Kirjeldus = request.Kirjeldus;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
