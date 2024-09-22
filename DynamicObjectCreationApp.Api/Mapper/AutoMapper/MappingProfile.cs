using AutoMapper;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Entity.Dto;
using Newtonsoft.Json;

namespace DynamicObjectCreationApp.Api.Mapper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddDynamicDataCommandRequest, DynamicObject>();
        }
    }
}
