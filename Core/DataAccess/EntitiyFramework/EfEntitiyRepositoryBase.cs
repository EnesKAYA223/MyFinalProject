using Core.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntitiyFramework
{
    public class EfEntitiyRepositoryBase<TEntity,TContext>:IEntitiyRepository<TEntity> 
        where TEntity : class,IEntitiy,new()
        where TContext:DbContext,new()
    {

        public void Add(TEntity entitiy)
        {

            using (TContext northwindContext = new TContext())
            {
                var addedEntity = northwindContext.Entry(entitiy);
                addedEntity.State = EntityState.Added;
                northwindContext.SaveChanges();
            }

        }

        public void Delete(TEntity entitiy)
        {

            using (TContext northwindContext = new TContext())
            {
                var deletedEntity = northwindContext.Entry(entitiy);
                deletedEntity.State = EntityState.Deleted;
                northwindContext.SaveChanges();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {


            using (TContext northwindContext = new TContext())
            {

                return filter == null ? northwindContext.Set<TEntity>().ToList() :
                    northwindContext.Set<TEntity>().Where(filter).ToList();
            }


        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {

            using (TContext northwindContext = new TContext())
            {

                return northwindContext.Set<TEntity>().SingleOrDefault(filter);
            }

        }

        public void Update(TEntity entitiy)
        {

            using (TContext northwindContext = new TContext())
            {
                var updatedEntity = northwindContext.Entry(entitiy);
                updatedEntity.State = EntityState.Modified;
                northwindContext.SaveChanges();
            }
        }

    }
}
