using AutoMapper;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicObjectCreationApp.Entity.Results;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.CommandHandlers
{
    public class UpdateDynamicDataCommandHandler : IRequestHandler<UpdateDynamicDataCommandRequest, Result<DynamicObject>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDynamicDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<DynamicObject>> Handle(UpdateDynamicDataCommandRequest request, CancellationToken cancellationToken)
        {

            var isExist = await _unitOfWork.DynamicObjectDal
      .Get(x => x.TableName.ToLower() == request.TableName.ToLower(), isTracking: false)
      .FirstOrDefaultAsync();
            var mapper = _mapper.Map<DynamicObject>(request);

            if (isExist is not null)
            {
                if (DeleteJsonObject(ref isExist, request.Id) != 1)
                    return Result.Fail(new ObjectTypeNotFound());
                var result = await _unitOfWork.DynamicObjectDal.UpdateAsync(isExist);
                return Result.Ok(result);
            }
            else
            {
                var result = await _unitOfWork.DynamicObjectDal.AddAsync(mapper);
                return Result.Fail(new TableNotFound());
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
