using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Projektid
{
    public class GetProjektQueryHandler : IRequestHandler<GetProjektQuery, OperationResult<Projekt_DTO>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProjektQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Projekt_DTO>> Handle(GetProjektQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Projekt_DTO>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Projektid
                .Where(list => list.Id == request.Id)
                .Select(list => new Projekt_DTO
                {
                    Id = list.Id,
                    Nimi = list.Nimi,
                    Kirjeldus = list.Kirjeldus,
                    Alguskuupaev = list.Alguskuupaev,
                    Lopetatuskuupaev = list.Lopetatuskuupaev,
                    Ylesanded = list.Ylesanded,
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
