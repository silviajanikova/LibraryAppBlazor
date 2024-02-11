using LibraryAppBlazor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace LibraryAppBlazor.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContextFactory<LibraryAppContext> dbContextFactory;

        public Repository(IDbContextFactory<LibraryAppContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public virtual T GetById(Guid id)
        {
            using(var context = dbContextFactory.CreateDbContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public virtual List<T> List() {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return context.Set<T>().AsNoTracking().ToList();
            }
        }

        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                return context.Set<T>()
                       .AsNoTracking()
                       .Where(predicate)
                       .AsEnumerable();
            }
        }
        public int Add(T entity)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges();
            }
        }

        public int Edit(T entity)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                return context.SaveChanges();
            }
        }

        public int Delete(T entity)
        {
            using (var context = dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Remove(entity);
                return context.SaveChanges();
            }
        }
    }
}
