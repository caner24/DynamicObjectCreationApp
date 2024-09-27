using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Dto
{
    public record FieldDto
    {
        public string? FieldName { get; init; }
        public string? FieldValue { get; init; }
    }

}
