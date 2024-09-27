using DynamicObjectCreationApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Abstract
{
    public interface IDynamicRepository
    {
        Task CreateTableAsync(DynamicObject objectDefinition);
        Task<int> CreateAsync(string objectName, Dictionary<string, object> data);
        Task<Dictionary<string, object>> GetByIdAsync(string objectName, int id);
        Task UpdateAsync(string objectName, int id, Dictionary<string, object> data);
        Task DeleteAsync(string objectName, int id);
        Task<List<Dictionary<string, object>>> GetAllAsync(string objectName, Dictionary<string, object> filters = null);
    }
}
