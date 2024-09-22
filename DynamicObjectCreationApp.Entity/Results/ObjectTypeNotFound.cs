using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Results
{
    public class ObjectTypeNotFound : Error
    {
        public ObjectTypeNotFound() : base("The object type was not found !.")
        {

        }
    }
}
