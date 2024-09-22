using AutoMapper;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using FluentResults;
using MediatR;
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
            var mapper = _mapper.Map<DynamicObject>(request);
            AddIdIfNotExists(ref mapper);

            var result = await _unitOfWork.DynamicObjectDal.AddAsync(mapper);
            return Result.Ok(result);
        }
        private void AddIdIfNotExists(ref DynamicObject jsonObject)
        {
            var jsonObjectDes = JsonConvert.DeserializeObject<JObject>(jsonObject.ObjectDataJson.ToString());
            if (!jsonObjectDes.ContainsKey("id"))
            {
                jsonObjectDes["id"] = Guid.NewGuid();
            }
            jsonObject.ObjectDataJson = jsonObjectDes;
        }
    }
}
