using System.Threading.Tasks;
using KooliProjekt.Application.Features.Ylesanded;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class YlesandedController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        
        public YlesandedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListYlesandedQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }
    }
}
