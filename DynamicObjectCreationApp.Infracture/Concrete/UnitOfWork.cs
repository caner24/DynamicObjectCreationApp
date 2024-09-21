using DynamicObjectCreationApp.Entity.Exceptions;
using DynamicObjectCreationApp.Infracture.Abstract;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Concrete
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IDynamicObejctDal _dynamicObjectDal;
        private readonly DynamicContext _dynamicContext;



        public UnitOfWork(IDynamicObejctDal dynamicObjectDal, DynamicContext dynamicContext)
        {
            _dynamicObjectDal = dynamicObjectDal;
            _dynamicContext = dynamicContext;

        }
        public IDynamicObejctDal DynamicObjectDal => _dynamicObjectDal;
        public async Task BeginTransactionAsync()
        {
            if (_dynamicContext.Database.CurrentTransaction == null)
            {
                Log.Information("Transaction creating . . .");
                await _dynamicContext.Database.BeginTransactionAsync();
                Log.Information("Transaction created . . .");
            }
        }
        public async Task CommitAsync()
        {
            if (_dynamicContext.Database.CurrentTransaction != null)
            {
                Log.Information("Transaction commiting . . .");
                await _dynamicContext.Database.RollbackTransactionAsync();
                Log.Information("Transaction commited . . .");
            }
            else
            {
                throw new TransactionNotFound("Commit");
            }
        }

        public async Task RollbackAsync()
        {
            if (_dynamicContext.Database.CurrentTransaction != null)
            {
                Log.Information("Transaction rollbacking . . .");
                await _dynamicContext.Database.RollbackTransactionAsync();
                Log.Information("Transaction rollbacked . . .");
            }
            else
            {
                throw new TransactionNotFound("Rollback");
            }
        }
        public void Dispose()
        {
            _dynamicContext.Dispose();
        }
    }
}
