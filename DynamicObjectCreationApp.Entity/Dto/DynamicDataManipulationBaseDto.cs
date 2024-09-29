using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Dto
{
    public abstract record DynamicDataManipulationBaseDto
    {
        public int Id { get; init; }
        public string? ObjectName { get; init; }
    }
}
