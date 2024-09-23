using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Dto
{
    public abstract record DynamicDataManipulationBaseDto
    {
        public string? TableName { get; init; }
        public string? Id { get; init; }
    }
}
