using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Entity.Dto;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Queries.Request
{
    public record GetDynamicDataByIdQueryRequest : GetDynamicDataByIdDto, IRequest<Result<DynamicObject>>
    {

    }
}
