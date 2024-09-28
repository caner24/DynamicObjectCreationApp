using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete
{
    public class DynamicRepository : IDynamicRepository
    {
        private readonly IDynamicObjectDal _dynamicObjectDal;
        private readonly DynamicContext _dynamicContext;

        public DynamicRepository(IDynamicObjectDal dynamicObjectDal, DynamicContext dynamicContext)
        {
            _dynamicObjectDal = dynamicObjectDal;
            _dynamicContext = dynamicContext;
        }
        public async Task CreateTableAsync(DynamicObject objectDefinition)
        {
            var executionStrategy = _dynamicContext.Database.CreateExecutionStrategy();

            await executionStrategy.ExecuteAsync(async () =>
            {
                await _dynamicContext.Database.BeginTransactionAsync();
                try
                {
                    var tableName = objectDefinition.TableName;
                    var fields = objectDefinition.Fields;

                    var sql = $"CREATE TABLE {tableName} (Id INT AUTO_INCREMENT PRIMARY KEY, ";

                    foreach (var field in fields)
                    {
                        sql += $"{field.Name} {GetSqlDataType(field.DataType)} {(field.IsRequired ? "NOT NULL" : "NULL")}, ";
                    }

                    sql = sql.TrimEnd(',', ' ') + ")";

                    await _dynamicContext.Database.ExecuteSqlRawAsync(sql);
                    await _dynamicContext.DynamicObjects.AddAsync(new DynamicObject { Name = objectDefinition.Name, TableName = tableName });
                    foreach (var item in objectDefinition.Fields)
                    {
                        await _dynamicContext.FieldEntity.AddAsync(new FieldEntity { Name = item.Name, DataType = item.DataType, IsRequired = item.IsRequired, ObjectId = item.ObjectId });
                    }
                    await _dynamicContext.SaveChangesAsync();
                    await _dynamicContext.Database.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _dynamicContext.Database.RollbackTransactionAsync();
                }
            });
        }

        public async Task<int> CreateAsync(string objectName, Dictionary<string, object> data)
        {
            var objectDefinition = await _dynamicObjectDal.GetByNameAsync(objectName);
            if (objectDefinition == null)
                throw new ArgumentException($"Object definition '{objectName}' not found.");

            await ValidateDataAsync(objectDefinition, data);

            var tableName = objectDefinition.TableName;
            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(k => $"@{k}"));
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT LAST_INSERT_ID();";

            var parameters = data.Select(kvp => new MySqlParameter($"@{kvp.Key}", kvp.Value ?? DBNull.Value)).ToArray();

            return await ExecuteScalarAsync<int>(sql, parameters);
        }




        private async Task ValidateDataAsync(DynamicObject objectDefinition, Dictionary<string, object> data)
        {
            foreach (var field in objectDefinition.Fields)
            {
                if (field.IsRequired && (!data.ContainsKey(field.Name) || data[field.Name] == null))
                {
                    throw new ArgumentException($"Required field '{field.Name}' is missing.");
                }
            }
        }
        private async Task<T> ExecuteScalarAsync<T>(string sql, MySqlParameter[] parameters)
        {
            using var command = _dynamicContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddRange(parameters);
            command.Transaction = _dynamicContext.Database.CurrentTransaction?.GetDbTransaction();

            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return (T)Convert.ChangeType(result, typeof(T));
        }
        public Task<Dictionary<string, object>> GetByIdAsync(string objectName, int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string objectName, int id, Dictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string objectName, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Dictionary<string, object>>> GetAllAsync(string objectName, Dictionary<string, object> filters = null)
        {
            throw new NotImplementedException();
        }
        private string GetSqlDataType(string dataType)
        {
            return dataType.ToLower() switch
            {
                "string" => "VARCHAR(255)",
                "int" => "INT",
                "datetime" => "DATETIME",
                "bool" => "BIT",
                _ => "VARCHAR(255)"
            };
        }
    }
}
