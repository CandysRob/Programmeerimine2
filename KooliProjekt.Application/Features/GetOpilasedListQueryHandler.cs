using MediatR;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.OpilasedFeature
{
    // Handler, mis täidab päringu ja toob kõik õpilased andmebaasist
    public class GetOpilasedListQueryHandler : IRequestHandler<GetOpilasedListQuery, List<Opilased>>
    {
        private readonly ApplicationDbContext _context;

        public GetOpilasedListQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Opilased>> Handle(GetOpilasedListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Opilased.ToListAsync(cancellationToken);
        }
    }
}
