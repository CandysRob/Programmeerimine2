using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoLists
{
    /// <summary>
    /// 14.11.2025
    /// Listi kustutamise command
    /// </summary>
    public class DeleteToDoListCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
