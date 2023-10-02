using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public interface IService<T>
    {
        List<T> GetAll();
        T Get(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(int id);   
    }
}
