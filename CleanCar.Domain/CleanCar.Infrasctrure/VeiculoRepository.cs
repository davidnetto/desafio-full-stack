using Azure.Core;
using CleanCar.Application;
using CleanCar.Domain;
using CleanCar.Domain.DTOs.Veiculo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Infrastructure
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly VeiculoDbContext _veiculoDbContext;
        private readonly LocadoraDbContext _locadoraDbContext;
        private readonly ModeloDbContext _modeloDbContext;
        private readonly LogDbContext _logDbContext;

        public VeiculoRepository(VeiculoDbContext veiculoDbContext, LogDbContext logDbContext, LocadoraDbContext locadoraDbContext, ModeloDbContext modeloDbContext)
        {
            _veiculoDbContext = veiculoDbContext;
            _logDbContext = logDbContext;
            _locadoraDbContext = locadoraDbContext;
            _modeloDbContext = modeloDbContext;
        }

        public Veiculo Create(Veiculo veiculo)
        {
            veiculo.DataCriacao = DateTime.Now;
            _veiculoDbContext.Veiculos.Add(veiculo);
            _veiculoDbContext.SaveChanges();

            Log log = new Log()
            {
                VeiculoId = veiculo.Id,
                LocadoraId = veiculo.LocadoraId,
                DataInicio = veiculo.DataCriacao,
            };
            _logDbContext.Log.Add(log);
            _logDbContext.SaveChanges();

            return veiculo;
        }

        public List<Veiculo> GetAll()
        {
            return _veiculoDbContext.Veiculos.ToList();
        }

        public List<Veiculo> GetByFilter()
        {

            return _veiculoDbContext.Veiculos.ToList();
        }


        public Veiculo Get(int id)
        {
            var entity = _veiculoDbContext.Veiculos.Find(id);
            if (entity == null)
            {
                throw new Exception();
            }
            else
            {
                return entity;
            }
        }

        public List<VeiculoDTO> Get(VeiculoDTO dto)
        {
            List<VeiculoDTO> veiculoDTOs = new List<VeiculoDTO>();

            var query = _veiculoDbContext.Veiculos.AsQueryable();

            // Aplicar filtro por modelo
            if (dto.ModeloId > 0)
            {
                query = query.Where(veiculo => veiculo.ModeloId == dto.ModeloId);
            }

            // Aplicar filtro por data de criação
            if (dto.DataCriacaoInicio != default && dto.DataCriacaoFim != default)
            {
                query = query.Where(veiculo =>
                    veiculo.DataCriacao >= dto.DataCriacaoInicio && veiculo.DataCriacao <= dto.DataCriacaoFim);
            }

            // Aplicar filtro por nome da locadora
            if (!string.IsNullOrEmpty(dto.LocadoraNome))
            {
                var locadoraIds = _locadoraDbContext.Locadoras
                    .Where(locadora => locadora.NomeFantasia.Contains(dto.LocadoraNome))
                    .Select(locadora => locadora.Id)
                    .ToList();

                query = query.Where(veiculo => locadoraIds.Contains(veiculo.LocadoraId));
            }

            // Realizar a consulta final
            var resultados = query.ToList();

            // Buscar os dados de locadoras e modelos em lote
            var locadoraIdsDistinct = resultados.Select(veiculo => veiculo.LocadoraId).Distinct().ToList();
            var locadoras = _locadoraDbContext.Locadoras
                .Where(locadora => locadoraIdsDistinct.Contains(locadora.Id))
                .ToDictionary(locadora => locadora.Id, locadora => locadora.NomeFantasia);

            var modeloIdsDistinct = resultados.Select(veiculo => veiculo.ModeloId).Distinct().ToList();
            var modelos = _modeloDbContext.Modelos
                .Where(modelo => modeloIdsDistinct.Contains(modelo.ID))
                .ToDictionary(modelo => modelo.ID, modelo => modelo.Nome);

            // Mapear os resultados para DTOs
            foreach (var veiculo in resultados)
            {
                VeiculoDTO veiculoDTO = new VeiculoDTO
                {
                    LocadoraNome = locadoras.TryGetValue(veiculo.LocadoraId, out var locadoraNome) ? locadoraNome : null,
                    ModeloId = veiculo.ModeloId,
                    ModeloNome = modelos.TryGetValue(veiculo.ModeloId, out var modeloNome) ? modeloNome : null,
                    Placa = veiculo.Placa,
                    DataCriacaoInicio = veiculo.DataCriacao
                };

                veiculoDTOs.Add(veiculoDTO);
            }

            return veiculoDTOs;
        }

        public Veiculo Update(Veiculo entity)
        {
            using (var transaction = _veiculoDbContext.Database.BeginTransaction())
            {
                try
                {
                    var veiculo = _veiculoDbContext.Veiculos.FirstOrDefault(v => v.Id == entity.Id);

                    if (veiculo == null)
                    {
                        throw new InvalidOperationException("Veículo não encontrado");
                    }

                    if (veiculo.LocadoraId != entity.LocadoraId)
                    {
                        var log = _logDbContext.Log.FirstOrDefault(x => x.VeiculoId == entity.Id && x.DataFim == null);

                        if (log != null)
                        {
                            log.DataFim = DateTime.Now;
            
                        }

                        var newLog = new Log()
                        {
                            VeiculoId = entity.Id,
                            DataInicio = DateTime.Now,
                            LocadoraId = entity.LocadoraId
                        };
                        _logDbContext.Log.Add(newLog);
                        _logDbContext.SaveChanges();
                    }

                    _veiculoDbContext.Entry(veiculo).CurrentValues.SetValues(entity);
                    _veiculoDbContext.SaveChanges();

                    transaction.Commit();

                    return entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("Falha na atualização do veículo", ex);
                }
            }
        }
        
        public Veiculo Delete(int id)
        {
            var veiculo = Get(id);
            if (veiculo != null)
            {
                var logs = _logDbContext.Log.Where(x => x.VeiculoId == veiculo.Id).ToList();
                if (logs != null)
                {
                    _logDbContext.Log.RemoveRange(logs);
                    _logDbContext.SaveChanges();
                }

                _veiculoDbContext.Veiculos.Remove(veiculo);
                _veiculoDbContext.SaveChanges();

            }

            return veiculo ?? throw new InvalidOperationException("Montadora não encontrada");
        }

    }
}
