using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Tootajad
{
    public class ListTootajadQuery : IRequest<OperationResult<IList<Tootaja>>>
    {
    }
}
