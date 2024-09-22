using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicObjectCreationApp.Entity;

namespace DynamicObjectCreationApp.Infracture.Abstract
{
    public interface IDynamicObjectDal : IEFRepositoryBase<DynamicObject>
    {

    }
}
