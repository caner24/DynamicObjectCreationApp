using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity
{
    public class DynamicObject : IEntity
    {
        public DynamicObject()
        {
            ObjectDataJson = new List<object>();
        }
        public int Id { get; set; }
        public string? TableName { get; set; }
        public List<object> ObjectDataJson { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
