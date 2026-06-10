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

namespace KooliProjekt.Application.Features._Projektid
{
    public class SaveProjektCommandHandler : IRequestHandler<SaveProjektCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveProjektCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveProjektCommand request, CancellationToken cancellationToken)
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

            var list = new Projekt();
            if(request.Id == 0)
            {
                await _dbContext.Projektid.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Projektid.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.Nimi = request.Nimi;
            list.Kirjeldus = request.Kirjeldus;
            list.Alguskuupaev = request.Alguskuupaev;
            list.Lopetatuskuupaev = request.Lopetatuskuupaev;
            list.Ylesanded = request.Ylesanded;


            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
