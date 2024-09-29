using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Results
{
    public class DynamicObjectNotFound : Error
    {
        public DynamicObjectNotFound() : base("The dynamic object was not found")
        {

        }
    }
}
