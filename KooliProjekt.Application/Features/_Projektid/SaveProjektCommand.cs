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

namespace KooliProjekt.Application.Features._Projektid
{
    [ExcludeFromCodeCoverage]
    public class SaveProjektCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }

        public string Nimi { get; set; }

        public string Kirjeldus { get; set; }

        public DateTime Alguskuupaev { get; set; }

        public DateTime Lopetatuskuupaev { get; set; }

        public ICollection<Ylesanne> Ylesanded { get; set; }
    }
}
