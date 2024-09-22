using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Entity.Results;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.QueryHandlers
{
    public class GetAllDynamicQueryHandler : IRequestHandler<GetAllDynamicQueryRequest, Result<List<DynamicObject>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public GetAllDynamicQueryHandler(IUnitOfWork dynamicObjectDal)
        {
            _unitOfWork = dynamicObjectDal;
        }

        async Task<Result<List<DynamicObject>>> IRequestHandler<GetAllDynamicQueryRequest, Result<List<DynamicObject>>>.Handle(GetAllDynamicQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.DynamicObjectDal.GetAll(x => x.ObjectType == request.ObjectType).ToListAsync();

            if (result != null)
            {
                Log.Information("Data fetched succesfully.");
                return Result.Ok(result);
            }
            else
            {
                Log.Warning($"Data not found. ObjectType -> {request.ObjectType}");
                return Result.Fail(new ObjectTypeNotFound());
            }

        }
    }
}
