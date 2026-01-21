using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class GetTootajaQueryHandler : IRequestHandler<GetTootajaQuery, OperationResult<object>>
    {
        private readonly ITootajaRepository _tootajaRepository;

        public GetTootajaQueryHandler(ITootajaRepository tootajaRepository)
        {
            _tootajaRepository = tootajaRepository;
        }

        public async Task<OperationResult<object>> Handle(GetTootajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var tootaja = await _tootajaRepository.GetByIdAsync(request.Id);

            if (tootaja == null)
            {
                result.Value = null;
                return result;
            }

            result.Value = new
            {
                tootaja.Id,
                tootaja.Nimi,
                tootaja.Email,
                tootaja.Ametikoht
            };

            return result;
        }
    }
}
