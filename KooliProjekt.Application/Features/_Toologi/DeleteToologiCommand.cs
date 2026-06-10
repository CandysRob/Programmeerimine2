using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Toologi
{
    [ExcludeFromCodeCoverage]
    public class DeleteToologiCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
