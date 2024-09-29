using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Queries.Request
{
    public record GetDyanmicDataByIdQueryRequest : IRequest<Result<Dictionary<string, object>>>
    {
        public string? ObjectName { get; init; }
        public int Id { get; init; }
    }
}
