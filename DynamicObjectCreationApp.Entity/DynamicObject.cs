using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity
{
    public class DynamicObject:IEntity
    {
        public int Id { get; set; }
        public string? ObjectType { get; set; }
        public string? ObjectData { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
