using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Projektid
{
    public class SaveProjektCommandHandler : IRequestHandler<SaveProjektCommand, OperationResult>
    {
        private readonly IProjektRepository _projektRepository;

        public SaveProjektCommandHandler(IProjektRepository projektRepository)
        {
            _projektRepository = projektRepository;
        }

        public async Task<OperationResult> Handle(SaveProjektCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var projekt = new Projekt();
            if (request.Id != 0)
            {
                projekt = await _projektRepository.GetByIdAsync(request.Id);
            }

            projekt.Nimi = request.Nimi;
            projekt.Kirjeldus = request.Kirjeldus;
            projekt.Alguskuupaev = request.Alguskuupaev;
            projekt.Lopetatuskuupaev = request.Lopetatuskuupaev;

            await _projektRepository.SaveAsync(projekt);

            return result;
        }
    }
}
