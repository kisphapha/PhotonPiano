
using Microsoft.EntityFrameworkCore;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Models.Models;
using System.Linq.Expressions;

namespace PhotonPiano.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PhotonPianoContext _context;

        public GenericRepository(PhotonPianoContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().RemoveRange(enumerable);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                _context.Set<T>().UpdateRange(enumerable);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(long id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<T?> FindOneWithNoTrackingAsync(Expression<Func<T, bool>> expression)
        {
            var dbObject = await _context.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();
            return dbObject;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }
    }

}
