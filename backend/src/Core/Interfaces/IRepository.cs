using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}