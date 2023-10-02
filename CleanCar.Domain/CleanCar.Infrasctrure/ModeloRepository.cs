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
    public class ModeloRepository : IModeloRepository
    {
        private readonly ModeloDbContext _DbContext;

        public ModeloRepository(ModeloDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Modelo Create(Modelo modelo)
        {
            if (modelo.Nome != null)
            {
                ModeloDTO dto = new ModeloDTO() { Nome = modelo.Nome };
                var modelos = ListarModelosUnicas(dto);
                if (modelos.Count() > 0)
                {
                    throw new DuplicadaException("Já existe uma modelo com este nome.");

                }
                else
                {
                    modelo.Nome = modelo.Nome.RemoveDiacritics();

                    _DbContext.Modelos.Add(modelo);
                    _DbContext.SaveChanges();
                }
            }
            return modelo;
        }

        public List<Modelo> GetAll()
        {
            return _DbContext.Modelos.OrderBy(x => x.Nome).ToList();
        }

        public Modelo Get(int id)
        {
            var entity = _DbContext.Modelos.FirstOrDefault(x => x.ID == id);
            if (entity == null)
            {
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }
        public IEnumerable<Modelo> ListarModelosUnicas(ModeloDTO filtro)
        {
            IEnumerable<Modelo> modelosComNome = new List<Modelo>();
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                modelosComNome = _DbContext.Modelos.Where(m => m.Nome == filtro.Nome.RemoveDiacritics()).ToList();
            }

            return modelosComNome;
        }


        public Modelo Update(Modelo modelo)
        {
            _DbContext.Modelos.Update(modelo);
            _DbContext.SaveChanges();

            return modelo ?? throw new InvalidOperationException("Modelo não encontrada");
        }

        Modelo IRepository<Modelo>.Delete(int id)
        {
            var modelo = Get(id);
            if (modelo != null)
            {
                _DbContext.Modelos.Remove(modelo);
                _DbContext.SaveChanges();
            }

            return modelo ?? throw new InvalidOperationException("Modelo não encontrada");
        }
    }
}
