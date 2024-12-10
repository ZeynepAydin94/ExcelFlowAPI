using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExcelFlow.Core.Interfaces;

public interface IRepository
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}


