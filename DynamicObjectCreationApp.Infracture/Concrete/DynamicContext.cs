using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete
{
    public class DynamicContext : DbContext
    {

        public DynamicContext()
        {

        }
        public DynamicContext(DbContextOptions<DynamicContext> options) : base(options)
        {

        }

        public DbSet<IEntity> DynamicObjects { get; set; }
    }
}
