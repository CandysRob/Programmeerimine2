using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class DeleteYlesanneCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
