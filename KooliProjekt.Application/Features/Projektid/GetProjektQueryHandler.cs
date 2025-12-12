using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Projektid
{
    public class GetProjektQueryHandler : IRequestHandler<GetProjektQuery, OperationResult<object>>
    {
        private readonly IProjektRepository _projektRepository;

        public GetProjektQueryHandler(IProjektRepository projektRepository)
        {
            _projektRepository = projektRepository;
        }

        public async Task<OperationResult<object>> Handle(GetProjektQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var projekt = await _projektRepository.GetByIdAsync(request.Id);

            if (projekt == null)
            {
                result.Value = null;
                return result;
            }

            result.Value = new
            {
                projekt.Id,
                projekt.Nimi,
                projekt.Kirjeldus,
                projekt.Alguskuupaev,
                projekt.Lopetatuskuupaev
            };

            return result;
        }
    }
}
