using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Ylesanded
{
    public class SaveYlesanneCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Pealkiri { get; set; }
        public string Kirjeldus { get; set; }
        public DateTime Tahtaeg { get; set; }
        public string Staatus { get; set; }
        public decimal TunnidKokku { get; set; }
        public int ProjektId { get; set; }
        public int TootajaId { get; set; }
    }
}
