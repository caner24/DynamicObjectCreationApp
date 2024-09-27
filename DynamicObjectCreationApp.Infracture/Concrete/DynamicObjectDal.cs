using DynamicObjectCreationApp.Domain.Data.EntityFramework.Concrete;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete
{
    public class DynamicObjectDal : EFRepositoryBase<DynamicContext, DynamicObject>, IDynamicObjectDal
    {
        private readonly DynamicContext _context;
        public DynamicObjectDal(DynamicContext context) : base(context)
        {
            _context = context;
        }
        public async Task<DynamicObject> GetByNameAsync(string name)
        {
            return await _context.DynamicObjects
                .Include(o => o.Fields)
                .FirstOrDefaultAsync(o => o.Name == name);
        }
    }
}
