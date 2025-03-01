using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync();
    }
}
