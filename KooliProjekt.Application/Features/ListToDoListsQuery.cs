using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.TodoLists
{
    public class ListToDoListsQuery : IRequest<OperationResult<IList<ToDoList>>>
    {
    }
}
