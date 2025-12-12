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
	public class ListToologiHandler : IRequestHandler<ListToologi, OperationResult<PagedResult<Ylesanne>>>
	{
		private readonly ApplicationDbContext _dbContext;

		public ListToologiHandler(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<OperationResult<PagedResult<Ylesanne>>> Handle(ListToologi request, CancellationToken cancellationToken)
		{
			var result = new OperationResult<PagedResult<Ylesanne>>();

			// Order by name or other relevant field; adjust if your Ylesanne entity uses a different property
			result.Value = await _dbContext
				.Ylesanded
				.OrderBy(y => y.Pealkiri)
				.GetPagedAsync(request.Page, request.PageSize);

			return result;
		}
	}
}
