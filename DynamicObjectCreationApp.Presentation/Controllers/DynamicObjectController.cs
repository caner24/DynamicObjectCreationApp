using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Presentation.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
        [OutputCache(Duration =60)]
        public async Task<IActionResult> GetAllData([FromQuery] GetAllDynamicQueryRequest getAllDynamicQueryRequest)
        {
            var response = await _mediator.Send(getAllDynamicQueryRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
            {
                return NotFound(response.Errors);
            }
        }

        [HttpGet("getById")]
        [OutputCache(Duration = 60)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetById([FromQuery] GetDynamicDataByIdQueryRequest getDynamicDataByIdQueryRequest)
        {
            var response = await _mediator.Send(getDynamicDataByIdQueryRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
            {
                return NotFound(response.Errors);
            }
        }

        [HttpPost("addDynamicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDynamicData([FromBody] AddDynamicDataCommandRequest addDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(addDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500);
        }

        [HttpDelete("deleteDynamicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteDynamicData([FromBody] DeleteDynamicDataCommandRequest deleteDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(deleteDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok();
            else
                return NotFound(response.Errors);
        }

        [HttpPut("updateDynamicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDynamicData([FromBody] UpdateDynamicDataCommandRequest updateDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(updateDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500);
        }
    }
}
