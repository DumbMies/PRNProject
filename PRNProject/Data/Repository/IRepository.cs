using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRNProject.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
