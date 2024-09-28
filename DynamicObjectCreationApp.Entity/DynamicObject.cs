using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity
{
    public class DynamicObject : IEntity
    {
        public DynamicObject()
        {
            Fields = new List<FieldEntity>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TableName { get; set; }
        public List<FieldEntity> Fields { get; set; }

    }
}
