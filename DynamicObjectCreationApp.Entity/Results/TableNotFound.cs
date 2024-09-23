using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Results
{
    public class TableNotFound : Error
    {
        public TableNotFound() : base("The table type was not found !.")
        {

        }
    }
}
