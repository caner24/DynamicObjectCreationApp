using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Dto
{
    public record AddDynamicObjectDto
    {
        public string? ObjectType { get; init; }
        public object? ObjectDataJson { get; init; }
    }
}
