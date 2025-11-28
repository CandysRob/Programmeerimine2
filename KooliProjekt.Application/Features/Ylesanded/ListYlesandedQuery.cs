using System.Collections.Generic;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class ListYlesandedQuery : IRequest<OperationResult<IList<Ylesanne>>>
    {
    }
}
