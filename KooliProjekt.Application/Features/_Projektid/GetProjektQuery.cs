using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Projektid
{
    public class GetProjektQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
