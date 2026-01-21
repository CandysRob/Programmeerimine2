using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class SaveTootajaCommandHandler : IRequestHandler<SaveTootajaCommand, OperationResult>
    {
        private readonly ITootajaRepository _tootajaRepository;

        public SaveTootajaCommandHandler(ITootajaRepository tootajaRepository)
        {
            _tootajaRepository = tootajaRepository;
        }

        public async Task<OperationResult> Handle(SaveTootajaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var tootaja = new Tootaja();
            if (request.Id != 0)
            {
                tootaja = await _tootajaRepository.GetByIdAsync(request.Id);
            }

            tootaja.Nimi = request.Nimi;
            tootaja.Email = request.Email;
            tootaja.Ametikoht = request.Ametikoht;

            await _tootajaRepository.SaveAsync(tootaja);

            return result;
        }
    }
}
