using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class GetYlesanneQueryHandler : IRequestHandler<GetYlesanneQuery, OperationResult<object>>
    {
        private readonly IYlesanneRepository _ylesanneRepository;

        public GetYlesanneQueryHandler(IYlesanneRepository ylesanneRepository)
        {
            _ylesanneRepository = ylesanneRepository;
        }

        public async Task<OperationResult<object>> Handle(GetYlesanneQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var y = await _ylesanneRepository.GetByIdAsync(request.Id);

            if (y == null)
            {
                result.Value = null;
                return result;
            }

            result.Value = new
            {
                y.Id,
                y.Pealkiri,
                y.Kirjeldus,
                y.Tahtaeg,
                y.Staatus,
                y.TunnidKokku,
                ProjektId = y.ProjektId,
                TootajaId = y.TootajaId
            };

            return result;
        }
    }
}
