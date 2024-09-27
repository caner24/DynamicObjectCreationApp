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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDynamicRepository _dynamicRepository;
        private readonly IDynamicObjectDal _dynamicObjectDal;
        private readonly DynamicContext _dynamicContext;

        public UnitOfWork(IDynamicRepository dynamicRepository, IDynamicObjectDal dynamicObjectDal, DynamicContext dynamicContext)
        {
            _dynamicRepository = dynamicRepository;
            _dynamicObjectDal = dynamicObjectDal;
            _dynamicContext = dynamicContext;

        }
        public DynamicContext DynamicContext => _dynamicContext;
        public IDynamicRepository DynamicRepository => _dynamicRepository;
        public IDynamicObjectDal DynamicObjectDal => _dynamicObjectDal;
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
            Log.Information("Disposing . . .");
            _dynamicContext.Dispose();
        }
    }
}
