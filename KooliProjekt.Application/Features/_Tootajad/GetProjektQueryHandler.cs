using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using KooliProjekt.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class GetTootajaQueryHandler : IRequestHandler<GetTootajaQuery, OperationResult<Tootaja_DTO>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetTootajaQueryHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Tootaja_DTO>> Handle(GetTootajaQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Tootaja_DTO>();

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Id <= 0)
            {
                return result;
            }

            result.Value = await _dbContext
                .Tootajad
                .Where(list => list.Id == request.Id)
                .Select(list => new Tootaja_DTO
                {
                    Id = list.Id,
                    Nimi = list.Nimi,  
                    Ametikoht = list.Ametikoht,
                    Email = list.Email,
                    Ylesanded = list.Ylesanded,
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
