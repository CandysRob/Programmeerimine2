using System.Threading.Tasks;
using KooliProjekt.Application.Features.Projektid;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ProjektidController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProjektidController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] ListProjektidQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }
    }
}
