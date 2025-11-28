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
        public async Task<IActionResult> List()
        {
            var query = new ListProjektidQuery();
            var result = await _mediator.Send(query);

            return Result(result);
        }
    }
}
