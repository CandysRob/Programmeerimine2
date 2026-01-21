using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class GetTootajaQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
