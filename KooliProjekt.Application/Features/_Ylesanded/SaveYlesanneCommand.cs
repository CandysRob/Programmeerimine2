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

namespace KooliProjekt.Application.Features._Ylesanded
{
    [ExcludeFromCodeCoverage]
    public class SaveYlesanneCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Pealkiri { get; set; }
        public string Kirjeldus { get; set; }
        public DateTime Tahtaeg { get; set; }
        public string Staatus { get; set; }
        public decimal TunnidKokku { get; set; }
        public Projekt Projekt { get; set; }
        public int ProjektId { get; set; }
        public Tootaja Tootaja { get; set; }
        public int TootajaId { get; set; }
    }
}
