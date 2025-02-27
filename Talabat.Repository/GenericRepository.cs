using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();    
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).ToListAsync();
        }
        public async Task<T?> GetWithSpecsAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).FirstAsync();
        }
        public async Task<int> GetCountAsync(ISpecifications<T> specs)
        {
            return await ApplySpecification(specs).CountAsync();
        }
        private  IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationsEvaluator<T>.GetQuery(_context.Set<T>(), specifications);
        }
    }
}
