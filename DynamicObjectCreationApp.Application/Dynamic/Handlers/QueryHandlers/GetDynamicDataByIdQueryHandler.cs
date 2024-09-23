using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Entity.Results;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.QueryHandlers
{
    public record GetDynamicDataByIdQueryHandler : IRequestHandler<GetDynamicDataByIdQueryRequest, Result<DynamicObject>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDynamicDataByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<DynamicObject>> Handle(GetDynamicDataByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.DynamicObjectDal.GetAll(x => x.TableName == request.TableName).FirstOrDefaultAsync();

            if (result != null)
            {
                if (DeleteJsonObject(ref result, request.Id) != 1)
                    return Result.Fail(new ObjectTypeNotFound());

                return Result.Ok(result);
            }
            else
            {
                return Result.Fail(new ObjectTypeNotFound());
            }
        }
        private int DeleteJsonObject(ref DynamicObject dynamicObject, string id)
        {
            for (int i = dynamicObject.ObjectDataJson.Count - 1; i >= 0; i--)
            {
                if (dynamicObject.ObjectDataJson[i] == null)
                {
                    continue;
                }
                var jsonObject = JsonConvert.DeserializeObject<JObject>(dynamicObject.ObjectDataJson[i].ToString());
                if (jsonObject.ContainsKey("id") && jsonObject["id"].ToString() == id)
                {
                    dynamicObject.ObjectDataJson.RemoveAt(i);
                    return 1;
                }
            }
            return 0;
        }
    }
}
