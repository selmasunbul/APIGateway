using DataAccess.Base;
using System.Linq.Expressions;

namespace Business.Base
{
    public interface IServiceBase<TEntity>
        where TEntity : class, IEntityBase, new()

    {

    
        bool SoftDelete(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, List<string>? includes = null);
        Task<TEntity?> UpdateAsync(TEntity entity);

    }
}
