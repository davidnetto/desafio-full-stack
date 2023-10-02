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
    public class MontadoraRepository : IMontadoraRepository
    {
        private readonly MontadoraDbContext _DbContext;

        public MontadoraRepository(MontadoraDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public Montadora Create(Montadora montadora)
        {
            if (montadora.Nome != null)
            {
                MontadoraDTO dto = new MontadoraDTO() { Nome = montadora.Nome };
                var montadoras = ListarMontadorasUnicas(dto);
                if (montadoras.Count() > 0)
                {
                    throw new DuplicadaException("Já existe uma montadora com este nome.");

                }
                else
                {
                    montadora.Nome = montadora.Nome.RemoveDiacritics();

                    _DbContext.Montadoras.Add(montadora);
                    _DbContext.SaveChanges();
                }
            }

            return montadora;
        }

        public List<Montadora> GetAll()
        {
            return _DbContext.Montadoras.OrderBy(x => x.Nome).ToList();
        }

        public Montadora Get(int id)
        {
            var entity = _DbContext.Montadoras.FirstOrDefault(x => x.ID == id);
            if (entity == null)
            {
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }
        public IEnumerable<Montadora> ListarMontadorasUnicas(MontadoraDTO filtro)
        {
            IEnumerable<Montadora> montadorasComNome = new List<Montadora>();
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                montadorasComNome = _DbContext.Montadoras.Where(m => m.Nome == filtro.Nome.RemoveDiacritics()).ToList();
            }

            return montadorasComNome;
        }


        public Montadora Update(Montadora montadora)
        {
            _DbContext.Montadoras.Update(montadora);
            _DbContext.SaveChanges();

            return montadora ?? throw new InvalidOperationException("Montadora não encontrada");
        }

        Montadora IRepository<Montadora>.Delete(int id)
        {
            var montadora = Get(id);
            if (montadora != null)
            {
                _DbContext.Montadoras.Remove(montadora);
                _DbContext.SaveChanges();
            }

            return montadora ?? throw new InvalidOperationException("Montadora não encontrada");
        }

    }
}
