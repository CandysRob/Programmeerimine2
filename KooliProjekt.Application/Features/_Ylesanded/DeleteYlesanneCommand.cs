using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Ylesanded
{
    [ExcludeFromCodeCoverage]
    public class DeleteYlesanneCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
