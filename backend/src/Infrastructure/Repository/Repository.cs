using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Diagnostics;
using Core.Interfaces;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HouseholdDbContext _context;
        private DbSet<T> _entities;

        public Repository(HouseholdDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> InsertAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _entities.FindAsync(id);

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate) => await _entities.Where(predicate).ToListAsync();

        public async Task<T> UpdateAsync(T entity)
        {
            T? storedEntity = await _entities.FindAsync(entity.Id);
            if (storedEntity == null) throw new Exception("Entity to update not found");
            _context.Entry(storedEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0 ? entity : throw new Exception("Update failed");
        }
        /*
         * PUT is a method of modifying resource where the client sends data that updates the entire resource . 
         * PATCH is a method of modifying resources where the client sends partial data that is to be updated 
         * without modifying the entire data
         * 
         * */

        public async Task<bool> DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0 ? true : throw new Exception("Delete failed");
        }

    }
}