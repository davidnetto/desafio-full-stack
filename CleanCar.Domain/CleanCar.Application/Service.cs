using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CleanCar.Application
{
    public class Service<T> : IService<T>
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Create(T entity)
        {
            return _repository.Create(entity);
        }

        public T Delete(int id)
        {
            return _repository.Delete(id);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
