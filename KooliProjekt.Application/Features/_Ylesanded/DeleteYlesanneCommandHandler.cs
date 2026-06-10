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

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class DeleteYlesanneCommandHandler : IRequestHandler<DeleteYlesanneCommand, OperationResult>
    {
        private readonly ApplicationDbContext _db_context;

        public DeleteYlesanneCommandHandler(ApplicationDbContext db_context)
        {
            if (db_context == null)
            {
                throw new ArgumentNullException(nameof(db_context));
            }
            _db_context = db_context;
        }

        public async Task<OperationResult> Handle(DeleteYlesanneCommand request, CancellationToken cancellationToken)
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

            var ylesanne = await _db_context
                .Ylesanded
                .Where(a => a.Id == request.Id)
                .FirstOrDefaultAsync();

            if (ylesanne == null)
            {
                return result;
            }

            _db_context.Remove(ylesanne);

            await _db_context.SaveChangesAsync();

            return result;
        }
    }
}
