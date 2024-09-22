﻿using DynamicObjectCreationApp.Entity;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Queries.Request
{
    public record GetAllDynamicQueryRequest : IRequest<Result<List<DynamicObject>>>
    {
        public string ObjectType { get; set; }
    }
}
