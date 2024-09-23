using DynamicObjectCreationApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete.Configuration
{
    public class DynamicObjectConfiguration : IEntityTypeConfiguration<DynamicObject>
    {
        public void Configure(EntityTypeBuilder<DynamicObject> builder)
        {
            builder.HasAlternateKey(x => x.TableName);
            builder.Property(x => x.ObjectDataJson).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<object>>(v));
        }
    }
}
