using DataAccess.Base;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Base
{
    public class ServiseBase<TEntity, TContext> : IServiceBase<TEntity>
        where TEntity : class, IEntityBase, new()
        where TContext : DbContext, new()

    {

        public async Task<TEntity?> AddAsync(TEntity entity)
        {

            using (DBContext ctx = new())
            {
                await ctx.Set<TEntity>().AddAsync(entity);
                if (ctx.SaveChangesAsync().Result > 0)
                    return entity;
            }

            return null;
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, List<string>? includes = null)

        {
            using (DBContext ctx = new())
            {
                IQueryable<TEntity>? whereClause = predicate != null ? ctx.Set<TEntity>().Where(predicate).Where(x => !x.IsDeleted) : ctx.Set<TEntity>().Where(x => !x.IsDeleted);

                if (includes != null && includes.Count > 0)
                {
                    foreach (var include in includes)
                    {
                        whereClause = whereClause.Include(include);
                    }

                    return await whereClause.ToListAsync();

                }

                return await whereClause.ToListAsync();
            }
        }


        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            using (DBContext ctx = new())
            {
                return await ctx.Set<TEntity>().FirstOrDefaultAsync(predicate);
            }
        }



        public bool SoftDelete(Expression<Func<TEntity, bool>> predicate)
        {
            using (DBContext ctx = new())
            {
                TEntity? existingEntity = ctx.Set<TEntity>().FirstOrDefault(predicate);

                if (existingEntity != null)
                {
                    existingEntity.IsDeleted = true;

                    ctx.Set<TEntity>().Update(existingEntity);
                    return ctx.SaveChanges() > 0;
                }
            }

            return false;
        }



        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            using (DBContext ctx = new())
            {
                bool isEntityExists = await ctx.Set<TEntity>().AnyAsync(x => x.Id == entity.Id);

                if (isEntityExists)
                {
                    ctx.Set<TEntity>().Update(entity);
                    if (ctx.SaveChangesAsync().Result > 0)
                        return entity;
                }
            }

            return null;
        }


    }
}
