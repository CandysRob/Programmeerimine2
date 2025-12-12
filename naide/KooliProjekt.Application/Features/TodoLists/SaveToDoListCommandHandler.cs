using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class SaveToDoListCommandHandler : IRequestHandler<SaveToDoListCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveToDoListCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveToDoListCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new ToDoList();
            if(request.Id == 0)
            {
                await _dbContext.ToDoLists.AddAsync(list);
            }
            else
            {
                list = await _dbContext.ToDoLists.FindAsync(request.Id);
            }

            list.Title = request.Title;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
