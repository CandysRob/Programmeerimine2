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

namespace KooliProjekt.Application.Features._Toologi
{
    [ExcludeFromCodeCoverage]
    public class SaveToologiCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }

        public string Nimi { get; set; }

        public int starttime { get; set; }

        public int endtime { get; set; }
        public string Kirjeldus { get; set; }
    }
}
