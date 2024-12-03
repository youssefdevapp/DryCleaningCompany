using System.Linq.Expressions;

namespace DryCleaningCompany.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        void Update(T entity);
    }
}