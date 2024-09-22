using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class DynamicObjectController : ControllerBase
    {

        private readonly IMediator _mediator;
        public DynamicObjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getAllData")]
        public async Task<IActionResult> GetById([FromQuery] GetAllDynamicQueryRequest getAllDynamicQueryRequest)
        {
            var response = await _mediator.Send(getAllDynamicQueryRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
            {
                return NotFound(response.Errors);
            }
        }

        [HttpPost("addDynamicData")]
        public async Task<IActionResult> AddDynamicData([FromBody] AddDynamicDataCommandRequest addDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(addDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500);
        }
    }
}
