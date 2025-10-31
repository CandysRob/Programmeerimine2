using MediatR;
using System.Collections.Generic;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Opilased
{
    public class GetOpilasedListQuery : IRequest<List<Opilased>>
    {
    }
}
