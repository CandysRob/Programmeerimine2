using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features._Toologi
{
    public class DeleteToologiCommandHandler : IRequestHandler<DeleteToologiCommand, OperationResult>
    {
        private readonly ApplicationDbContext _db_context;

        public DeleteToologiCommandHandler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult> Handle(DeleteToologiCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            var toolog = await _db_context
                .Toologid
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (toolog == null)
            {
                return result;
            }

            _db_context.Remove(toolog);

            await _db_context.SaveChangesAsync();

            return result;
        }
    }
}
