﻿using AutoMapper;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.CommandHandlers
{
    public class AddDynamicDataCommandHandler : IRequestHandler<AddDynamicDataCommandRequest, Result<DynamicObject>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddDynamicDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<DynamicObject>> Handle(AddDynamicDataCommandRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.DynamicObjectDal
      .Get(x => x.TableName.ToLower() == request.TableName.ToLower(), isTracking: false)
      .FirstOrDefaultAsync();

            var mapper = _mapper.Map<DynamicObject>(request);

            AddIdIfNotExists(ref mapper);

            if (isExist is not null)
            {
                isExist.ObjectDataJson.AddRange(mapper.ObjectDataJson);
                var result = await _unitOfWork.DynamicObjectDal.UpdateAsync(isExist);
                return Result.Ok(result);
            }
            else
            {
                var result = await _unitOfWork.DynamicObjectDal.AddAsync(mapper);
                return Result.Ok(result);
            }

        }
        private void AddIdIfNotExists(ref DynamicObject dynamicObject)
        {

            for (int i = 0; i < dynamicObject.ObjectDataJson.Count; i++)
            {
                if (dynamicObject.ObjectDataJson[i] == null)
                {
                    continue; 
                }
                var jsonObject = JsonConvert.DeserializeObject<JObject>(dynamicObject.ObjectDataJson[i].ToString());
                if (!jsonObject.ContainsKey("id"))
                {
                    jsonObject["id"] = Guid.NewGuid();
                }
                dynamicObject.ObjectDataJson[i] = jsonObject;
            }
        }
    }
}
