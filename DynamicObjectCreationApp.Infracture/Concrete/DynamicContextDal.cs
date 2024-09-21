using DynamicObjectCreationApp.Domain.Data.EntityFramework.Concrete;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete
{
    public class DynamicContextDal : EFRepositoryBase<DynamicContext, DynamicObject>, IDynamicObejctDal
    {
        public DynamicContextDal(DynamicContext context) : base(context)
        {
        }
    }
}
