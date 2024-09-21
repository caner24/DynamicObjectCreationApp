using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Entity.Exceptions
{
    public class TransactionNotFound: Exception
    {
        public TransactionNotFound(string type):base($"You don't have any transaction to {type}.")
        {
            
        }
    }
}
