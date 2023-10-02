using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Create(T entity);

        T Get(int id);

        T Update(T entity);

        T Delete(int id);
    }
}
