using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class DeleteTootajaCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
