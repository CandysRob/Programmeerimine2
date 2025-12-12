using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.toologi
{
    public class GetToologiQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
