using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projektid
{
    public class ListProjektidQuery : IRequest<OperationResult<IList<Projekt>>>
    {
    }
}
