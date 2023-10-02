using CleanCar.Application;
using CleanCar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diacritics;
using Diacritics.Extensions;
using System.Data.SqlTypes;

namespace CleanCar.Infrastructure
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly EnderecoDbContext _DbContext;

        public EnderecoRepository(EnderecoDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Endereco Create(Endereco endereco)
        {
         
            _DbContext.Enderecos.Add(endereco);
            _DbContext.SaveChanges();


            return endereco;
        }

        public List<Endereco> GetAll()
        {
            return _DbContext.Enderecos.OrderBy(x => x.Rua).ToList();
        }

        public Endereco Get(int id)
        {
            var entity = _DbContext.Enderecos.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }

        public Endereco Update(Endereco endereco)
        {
            _DbContext.Enderecos.Update(endereco);
            _DbContext.SaveChanges();


            return endereco ?? throw new InvalidOperationException("Endereco não encontrada");
        }

        Endereco IRepository<Endereco>.Delete(int id)
        {
            var endereco = Get(id);
            if (endereco != null)
            {
                _DbContext.Enderecos.Remove(endereco);
                _DbContext.SaveChanges();
            }
            return endereco ?? throw new InvalidOperationException("Endereco não encontrada");
        }
    }
}
