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

namespace KooliProjekt.Application.Features._Ylesanded
{
    public class SaveYlesanneCommandHandler : IRequestHandler<SaveYlesanneCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveYlesanneCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveYlesanneCommand request, CancellationToken cancellationToken)
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

            var list = new Ylesanne();
            if(request.Id == 0)
            {
                await _dbContext.Ylesanded.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Ylesanded.FindAsync(request.Id);
                if (list == null)
                {
                    result.AddError("Cannot find list with Id " + request.Id);
                    return result;
                }
            }

            list.Pealkiri = request.Pealkiri;
            list.Kirjeldus = request.Kirjeldus;
            list.Tahtaeg = request.Tahtaeg;
            list.Staatus = request.Staatus;
            list.TunnidKokku = request.TunnidKokku;
            list.Projekt = request.Projekt;
            list.ProjektId = request.ProjektId;
            list.Tootaja = request.Tootaja;
            list.TootajaId = request.TootajaId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
