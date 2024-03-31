using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotonPiano.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task<T?> FindOneWithNoTrackingAsync(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }

}
