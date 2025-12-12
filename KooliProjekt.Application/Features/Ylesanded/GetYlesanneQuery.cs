using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class GetYlesanneQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
