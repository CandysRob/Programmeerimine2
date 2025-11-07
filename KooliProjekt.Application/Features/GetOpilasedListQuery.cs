using MediatR;
using KooliProjekt.Application.Data;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.OpilasedFeature
{
    public class GetOpilasedListQuery : IRequest<List<Opilased>>
    {
    }
}
