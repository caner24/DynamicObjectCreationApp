using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.CommandHandlers
{
    public class UpdateDynamicDataCommandHandler : IRequestHandler<UpdateDynamicDataCommandRequest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDynamicDataCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(UpdateDynamicDataCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.DynamicRepository.UpdateAsync(request.ObjectName, request.Id, request.Data);
            return Result.Ok("Updated !.");
        }
    }
}
