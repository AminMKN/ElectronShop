using System.Linq.Expressions;

namespace _01_Framework.Domain
{
    public interface IRepository<TKey, T> where T : class
    {
        List<T> Get();
        T Get(TKey id);
        void Create(T entity);
        void SaveChanges();
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
