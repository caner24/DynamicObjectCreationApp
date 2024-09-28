using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
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

        [HttpPost("addDyanmicData")]
        public async Task<IActionResult> AddDyanmicData([FromBody] AddDynamicDataCommandRequest addDynamicDataCommandRequest)
        {
            var response = await _mediator.Send(addDynamicDataCommandRequest);
            if (response.IsSuccess)
                return Ok(response.Value);
            else
                return StatusCode(500, response.Errors);
        }
        [HttpPost("createDynamicDatabase")]
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
