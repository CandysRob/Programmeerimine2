using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Projektid
{
    public class SaveProjektCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Kirjeldus { get; set; }
        public DateTime Alguskuupaev { get; set; }
        public DateTime Lopetatuskuupaev { get; set; }
    }
}
