using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.DTO;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features._Projektid
{
    [ExcludeFromCodeCoverage]
    public class GetProjektQuery : IRequest<OperationResult<Projekt_DTO>>
    {
        public int Id { get; set; }
    }
}
