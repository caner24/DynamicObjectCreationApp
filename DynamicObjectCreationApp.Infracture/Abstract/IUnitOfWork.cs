using DynamicObjectCreationApp.Infracture.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Infracture.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        DynamicContext DynamicContext { get; }
        IDynamicRepository DynamicRepository { get; }
        IDynamicObjectDal DynamicObjectDal { get; }

    }
}
