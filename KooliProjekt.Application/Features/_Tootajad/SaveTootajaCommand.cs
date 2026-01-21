using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class SaveTootajaCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Email { get; set; }
        public string Ametikoht { get; set; }
    }
}
