using System.Threading.Tasks;
using KooliProjekt.Application.Features.toologi;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ToologidController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ToologidController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListToologi query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }
    }
}
