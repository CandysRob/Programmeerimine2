using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Toologi
{
    public class GetToologiQueryHandler : IRequestHandler<GetToologiQuery, OperationResult<toologi_DTO>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetToologiQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<toologi_DTO>> Handle(GetToologiQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<toologi_DTO>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Toologid
                .Where(list => list.Id == request.Id)
                .Select(list => new toologi_DTO
                {
                    Id = list.Id,
                    Nimi = list.Nimi,
                    Kirjeldus = list.Kirjeldus,
                    endtime = list.endtime,
                    starttime = list.starttime
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
