using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.toologi
{
	public class ListToologi : IRequest<OperationResult<PagedResult<toologi>>>
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
	}
}
