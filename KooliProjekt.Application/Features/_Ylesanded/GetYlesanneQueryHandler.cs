using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class GetYlesanneQueryHandler : IRequestHandler<GetYlesanneQuery, OperationResult<Ylesanne_DTO>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetYlesanneQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Ylesanne_DTO>> Handle(GetYlesanneQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Ylesanne_DTO>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Ylesanded
                .Where(list => list.Id == request.Id)
                .Select(list => new Ylesanne_DTO
                {
                    Id = list.Id,
                    Kirjeldus = list.Kirjeldus,
                    Pealkiri = list.Pealkiri,
                    ProjektId = list.ProjektId,
                    Staatus = list.Staatus,
                    Tahtaeg = list.Tahtaeg,
                    TootajaId = list.TootajaId,
                    TunnidKokku = list.TunnidKokku,
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
