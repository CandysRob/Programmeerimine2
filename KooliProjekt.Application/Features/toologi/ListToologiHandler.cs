using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.toologi
{
	public class ListToologiHandler : IRequestHandler<ListToologi, OperationResult<PagedResult<toologi>>>
	{
		private readonly ApplicationDbContext _dbContext;

		public ListToologiHandler(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<OperationResult<PagedResult<toologi>>> Handle(ListToologi request, CancellationToken cancellationToken)
		{
			var result = new OperationResult<PagedResult<toologi>>();

			result.Value = await _dbContext
				.Toologid
				.OrderBy(t => t.Nimi)
				.GetPagedAsync(request.Page, request.PageSize);

			return result;
		}
	}
}
