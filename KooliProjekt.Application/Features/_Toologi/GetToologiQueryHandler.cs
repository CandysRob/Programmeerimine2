using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Toologi
{
    public class GetToologiQueryHandler : IRequestHandler<GetToologiQuery, OperationResult<object>>
    {
        private readonly IToologiRepository _toologiRepository;

        public GetToologiQueryHandler(IToologiRepository toologiRepository)
        {
            _toologiRepository = toologiRepository;
        }

        public async Task<OperationResult<object>> Handle(GetToologiQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();
            var t = await _toologiRepository.GetByIdAsync(request.Id);

            if (t == null)
            {
                result.Value = null;
                return result;
            }

            result.Value = new
            {
                t.Id,
                t.Nimi,
                t.starttime,
                t.endtime,
                t.Kirjeldus
            };

            return result;
        }
    }
}
