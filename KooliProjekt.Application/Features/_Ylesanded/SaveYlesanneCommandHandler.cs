using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class SaveYlesanneCommandHandler : IRequestHandler<SaveYlesanneCommand, OperationResult>
    {
        private readonly IYlesanneRepository _ylesanneRepository;

        public SaveYlesanneCommandHandler(IYlesanneRepository ylesanneRepository)
        {
            _ylesanneRepository = ylesanneRepository;
        }

        public async Task<OperationResult> Handle(SaveYlesanneCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var ylesanne = new Ylesanne();
            if (request.Id != 0)
            {
                ylesanne = await _ylesanneRepository.GetByIdAsync(request.Id);
            }

            ylesanne.Pealkiri = request.Pealkiri;
            ylesanne.Kirjeldus = request.Kirjeldus;
            ylesanne.Tahtaeg = request.Tahtaeg;
            ylesanne.Staatus = request.Staatus;
            ylesanne.TunnidKokku = request.TunnidKokku;
            ylesanne.ProjektId = request.ProjektId;
            ylesanne.TootajaId = request.TootajaId;

            await _ylesanneRepository.SaveAsync(ylesanne);

            return result;
        }
    }
}
