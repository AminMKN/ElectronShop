using _01_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _01_Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(TKey id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }
    }
}