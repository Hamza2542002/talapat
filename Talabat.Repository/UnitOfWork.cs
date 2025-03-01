using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Dictionary<string, GenericRepository<BaseEntity>> _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repositories = new();
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (_repositories.ContainsKey(key))
            {
                return _repositories.GetValueOrDefault(key) as IGenericRepository<TEntity>;
            }
            var repo = new GenericRepository<TEntity>(_context) as GenericRepository<BaseEntity>;

            _repositories.Add(key, repo);

            return repo as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
           => await _context.DisposeAsync();
        

    }
}
