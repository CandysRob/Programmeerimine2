using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Projektid
{
    [ExcludeFromCodeCoverage]
    public class DeleteProjektCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
