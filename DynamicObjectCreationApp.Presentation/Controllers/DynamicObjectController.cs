using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
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
        private readonly IUnitOfWork _unitOfWork;
        public DynamicObjectController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("getAllDynamicData")]
        public async Task<IActionResult> GetAllDynamicData([FromQuery] GetDynamicQueryRequest getDynamicQueryRequest)
        {
            var response = await _mediator.Send(getDynamicQueryRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500, response.Errors);
        }

        [HttpGet($"getDynamicDataById/{nameof(GetDyanmicDataByIdQueryRequest.ObjectName)}/{nameof(GetDyanmicDataByIdQueryRequest.Id)}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> GetDynamicDataById([FromQuery] GetDyanmicDataByIdQueryRequest getDyanmicDataByIdQueryRequest)
        {
            var response = await _mediator.Send(getDyanmicDataByIdQueryRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500, response.Errors);
        }

        [HttpPost("addDyanmicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddDyanmicData([FromBody] AddDynamicDataCommandRequest addDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(addDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500, response.Errors);
        }

        [HttpPut("updateDynamicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDynamicData([FromBody] UpdateDynamicDataCommandRequest updateDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(updateDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500, response.Errors);
        }

        [HttpDelete("deleteDynamicData")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> DeleteDynamicData([FromBody] DeleteDynamicDataCommandRequest deleteDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(deleteDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response);
            else
                return StatusCode(500, response.Errors);
        }

        [HttpPost("createDynamicDatabase")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDynamicDatabase([FromBody] DynamicObject dynamicObject)
        {
            try
            {
                await _unitOfWork.DynamicRepository.CreateTableAsync(dynamicObject);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
    }
}
