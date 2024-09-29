using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity.Results;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.QueryHandlers
{
    internal class GetDynamicDataByIdQueryHandler : IRequestHandler<GetDyanmicDataByIdQueryRequest, Result<Dictionary<string, object>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDynamicDataByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Dictionary<string, object>>> Handle(GetDyanmicDataByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.DynamicRepository.GetByIdAsync(request.ObjectName, request.Id);
            if (result is not null)
                return Result.Ok(result);

            return Result.Fail(new DynamicObjectNotFound());
        }
    }
}
