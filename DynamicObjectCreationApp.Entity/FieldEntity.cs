using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity
{
    public class FieldEntity
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsRequired { get; set; }
        public DynamicObject Object { get; set; }
    }
}
