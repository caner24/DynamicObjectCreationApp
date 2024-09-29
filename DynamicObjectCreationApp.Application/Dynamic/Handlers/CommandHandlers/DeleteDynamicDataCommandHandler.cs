using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
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
    public class DeleteDynamicDataCommandHandler : IRequestHandler<DeleteDynamicDataCommandRequest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDynamicDataCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(DeleteDynamicDataCommandRequest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.DynamicRepository.DeleteAsync(request.ObjectName, request.Id);
            return Result.Ok("Deleted !.");
        }
    }
}
