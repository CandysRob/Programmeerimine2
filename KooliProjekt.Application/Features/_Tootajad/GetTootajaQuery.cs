using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Application.DTO;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Features._Tootajad
{
    [ExcludeFromCodeCoverage]
    public class GetTootajaQuery : IRequest<OperationResult<Tootaja_DTO>>
    {
        public int Id { get; set; }
    }
}
