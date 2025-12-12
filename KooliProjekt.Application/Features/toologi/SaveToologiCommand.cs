using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.toologi
{
    public class SaveToologiCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public int starttime { get; set; }
        public int endtime { get; set; }
        public string Kirjeldus { get; set; }
    }
}
