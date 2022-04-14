using CrudAPI.Data;
using CrudAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private UserProfileDbContext _dbContext;
        private DbSet<T> table;

        public GenericRepository(UserProfileDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
                
        }

        public void Delete(object Id)
        {
            T exists = table.Find(Id);
            table.Remove(exists);
        }


       

        public List<T> GetAll()
        {
            //  throw new NotImplementedException();
            return table.ToList();


        }



        public T GetByID(object Id)
        {
            return table.Find(Id);
           // throw new NotImplementedException();
        }

        public void Insert(T obj)
        {

            table.Add(obj);
          
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            // throw new NotImplementedException();
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

    }
}
