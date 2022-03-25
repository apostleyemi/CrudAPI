using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudAPI.Interfaces
{
   public  interface IGenericRepository<T> where T:class
    {

        List<T> GetAll();

        T GetByID(object Id);

        
     

        void Insert(T obj);

        void Update(T obj);

        void Delete(object Id);

        void Save();

      
    }
}
