using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Toologi
{
    public class DeleteToologiCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
