using MediatR;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.OpilasedFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpilasedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OpilasedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Opilased>>> GetOpilased()
        {
            var result = await _mediator.Send(new GetOpilasedListQuery());
            return Ok(result);
        }
    }
}
