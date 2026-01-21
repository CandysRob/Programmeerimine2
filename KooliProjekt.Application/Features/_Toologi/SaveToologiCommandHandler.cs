using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Toologi
{
    public class SaveToologiCommandHandler : IRequestHandler<SaveToologiCommand, OperationResult>
    {
        private readonly IToologiRepository _toologiRepository;

        public SaveToologiCommandHandler(IToologiRepository toologiRepository)
        {
            _toologiRepository = toologiRepository;
        }

        public async Task<OperationResult> Handle(SaveToologiCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var toologi = new toologi();
            if (request.Id != 0)
            {
                toologi = await _toologiRepository.GetByIdAsync(request.Id);
            }

            toologi.Nimi = request.Nimi;
            toologi.starttime = request.starttime;
            toologi.endtime = request.endtime;
            toologi.Kirjeldus = request.Kirjeldus;

            await _toologiRepository.SaveAsync(toologi);

            return result;
        }
    }
}
