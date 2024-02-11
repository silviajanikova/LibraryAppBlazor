using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;

namespace LibraryAppBlazor.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid Id);
        List<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        int Add(T entity);
        int Delete(T entity);

        int Edit(T entity);
    }
}
