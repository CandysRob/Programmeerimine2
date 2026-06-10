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

namespace KooliProjekt.Application.Features._Toologi
{
    public class SaveToologiCommandHandler : IRequestHandler<SaveToologiCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveToologiCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveToologiCommand request, CancellationToken cancellationToken)
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

            var list = new toologi();
            if(request.Id == 0)
            {
                await _dbContext.Toologid.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Toologid.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.Nimi = request.Nimi;
            list.Kirjeldus = request.Kirjeldus;
            list.starttime = request.starttime;
            list.endtime = request.endtime;


            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
