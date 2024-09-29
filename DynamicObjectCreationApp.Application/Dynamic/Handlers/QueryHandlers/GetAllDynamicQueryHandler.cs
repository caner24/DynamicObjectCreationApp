using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity.Results;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.QueryHandlers
{
    internal class GetAllDynamicQueryHandler : IRequestHandler<GetDynamicQueryRequest, Result<List<Dictionary<string, object>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDynamicQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Dictionary<string, object>>>> Handle(GetDynamicQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.DynamicRepository.GetAllAsync(request.ObjectName, request.Filters);
            if (result is not null)
                return Result.Ok(result);

            return Result.Fail(new DynamicObjectNotFound());

        }
    }
}
