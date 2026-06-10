using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features._Tootajad
{
    [ExcludeFromCodeCoverage]
    public class SaveTootajaCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Email { get; set; }
        public string Ametikoht { get; set; }
        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
