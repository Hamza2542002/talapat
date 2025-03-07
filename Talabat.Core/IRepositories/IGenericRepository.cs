using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetWithSpecsAsync(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications);
        Task<int> GetCountAsync(ISpecifications<T> specs);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
