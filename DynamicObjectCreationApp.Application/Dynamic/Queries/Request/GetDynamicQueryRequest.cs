using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Queries.Request
{
    public record GetDynamicQueryRequest : IRequest<Result<List<Dictionary<string, object>>>>
    {
        public string? ObjectName { get; set; }
        public Dictionary<string, object>? Filters { get; init; }
    }
}
