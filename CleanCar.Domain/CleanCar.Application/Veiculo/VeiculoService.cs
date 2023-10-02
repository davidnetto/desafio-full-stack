using CleanCar.Domain;
using CleanCar.Domain.DTOs.Veiculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Application
{
    public class VeiculoService : Service<Veiculo>, IVeiculoService
    {
        private readonly IVeiculoRepository _repositoryVeiculo;
        public VeiculoService(IVeiculoRepository veiculoRepository, IVeiculoRepository repositoryVeiculo) : base(veiculoRepository)
        {

            _repositoryVeiculo = repositoryVeiculo;
        }

        public List<VeiculoDTO> Get(VeiculoDTO dto)
        {
            return _repositoryVeiculo.Get(dto);
        }
    }
}
