using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Validators;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features._Tootajad
{
    public class SaveTootajaCommandHandler : IRequestHandler<SaveTootajaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveTootajaCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveTootajaCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();
            if (request.Id < 0)
            {
                result.AddPropertyError(nameof(request.Id), "Id cannot be negative");
                return result;
            }

            var list = new Tootaja();
            if(request.Id == 0)
            {
                await _dbContext.Tootajad.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Tootajad.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.Nimi = request.Nimi;
            list.Ametikoht = request.Ametikoht;
            list.Ylesanded = request.Ylesanded;
            list.Email = request.Email;


            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
