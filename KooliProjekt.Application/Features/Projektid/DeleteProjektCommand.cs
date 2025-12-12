using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projektid
{
    public class DeleteProjektCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
