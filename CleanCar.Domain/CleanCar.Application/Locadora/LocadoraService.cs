using CleanCar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public class LocadoraService : Service<Locadora>, ILocadoraService
    {
        private readonly ILocadoraRepository _repository;

        public LocadoraService(ILocadoraRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Locadora Create(LocadoraDTO dto)
        {
            return _repository.Create(dto);
        }

        public LocadoraDTO Get(LocadoraDTO dto)
        {
            return _repository.Get(dto);
        }

        public List<ModeloDTO> GetRelatorio()
        {
           return _repository.GetRelatorio();
        }

        public Locadora Update(LocadoraDTO dto)
        {
            return _repository.Update(dto);
        }

    }
}

