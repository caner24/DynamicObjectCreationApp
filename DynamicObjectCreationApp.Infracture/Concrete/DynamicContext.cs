using DynamicObjectCreationApp.Domain.Data.EntityFramework.Abstract;
using DynamicObjectCreationApp.Entity;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DynamicContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DynamicObject> DynamicObjects { get; set; }
    }
}
