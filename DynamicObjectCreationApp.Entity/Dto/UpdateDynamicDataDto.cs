using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Dto
{
    public record UpdateDynamicDataDto : DynamicDataManipulationBaseDto
    {
        public Dictionary<string, object>? Data { get; init; }
    }
}
