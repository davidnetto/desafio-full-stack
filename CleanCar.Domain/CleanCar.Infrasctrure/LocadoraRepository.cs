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
using System.Security.Principal;

namespace CleanCar.Infrastructure
{
    public class LocadoraRepository : ILocadoraRepository
    {
        private readonly LocadoraDbContext _DbContext;
        private readonly EnderecoDbContext _DbContextEndereco;
        private readonly VeiculoDbContext _DbContextVeiculo;
        private readonly ModeloDbContext _DbContextModelo;

        public LocadoraRepository(LocadoraDbContext DbContext, EnderecoDbContext EnderecoDbContext, VeiculoDbContext dbContextVeiculo, ModeloDbContext dbContextModelo)
        {
            _DbContext = DbContext;
            _DbContextEndereco = EnderecoDbContext;
            _DbContextVeiculo = dbContextVeiculo;
            _DbContextModelo = dbContextModelo;
        }

        public Locadora Create(Locadora locadora)
        {
            _DbContext.Locadoras.Add(locadora);
            _DbContext.SaveChanges();
            return locadora;
        }

        public List<Locadora> GetAll()
        {
            return _DbContext.Locadoras.OrderBy(x => x.NomeFantasia).ToList();
        }

        public List<ModeloDTO> GetRelatorio()
        {
            var modelos = _DbContextModelo.Modelos.ToList();
            var veiculos = _DbContextVeiculo.Veiculos.ToList();
            var locadoras = _DbContext.Locadoras.ToList();

            var locadorasDTO = new List<LocadoraDTO>();
            var modelosDTO = new List<ModeloDTO>();

            foreach (var locadora in locadoras)
            {
                var veiculosLocadora = veiculos.Where(x => x.LocadoraId == locadora.Id).ToList();

              
                foreach (var modelo in modelos)
                {
                    var quantidadeVeiculos = veiculosLocadora.Count(x => x.ModeloId == modelo.ID);

                    if (quantidadeVeiculos > 0)
                    {
                        modelosDTO.Add(new ModeloDTO()
                        {
                            LocadoraNome = locadora.NomeFantasia,
                            Nome = modelo.Nome,
                            QuantidadeVeiculosPorModelo = quantidadeVeiculos
                        });
                    }
                }
            }

            return modelosDTO;
        }


        public LocadoraDTO Get(LocadoraDTO dtoLocadora)
        {
            var entity = _DbContext.Locadoras.Find(dtoLocadora.Id);

            if (entity != null)
            {
                dtoLocadora = new LocadoraDTO()
                {
                    Id = entity.Id,
                    NomeFantasia = entity.NomeFantasia,
                    RazaoSocial = entity.RazaoSocial,
                    Telefone = entity.Telefone,
                    Email = entity.Email,
                    CNPJ = entity.CNPJ,
                };

                var endereco = _DbContextEndereco.Enderecos.Where(x => x.LocadoraId.Equals(dtoLocadora.Id)).FirstOrDefault();
                //var endereco = _DbContextEndereco.Enderecos.Where(x => x.LocadoraId == entity.Id).FirstOrDefault();
                if (endereco != null)
                {
                    dtoLocadora.Bairro = endereco.Bairro;
                    dtoLocadora.Rua = endereco.Rua;
                    dtoLocadora.Numero = endereco.Numero;
                    dtoLocadora.Cidade = endereco.Cidade;
                    dtoLocadora.Estado = endereco.Estado;
                    dtoLocadora.Cep = endereco.CEP;
                }
                _DbContextEndereco.SaveChanges();


                if (dtoLocadora == null)
                {
                    throw new Exception();
                }
                else
                {
                    return dtoLocadora;
                }
            }
            return dtoLocadora ?? throw new InvalidOperationException("Locadora não encontrada");
        }

        public IEnumerable<Locadora> ListarLocadorasUnicas(LocadoraDTO filtro)
        {
            IEnumerable<Locadora> locadorasComNome = new List<Locadora>();
            if (!string.IsNullOrEmpty(filtro.NomeFantasia))
            {
                locadorasComNome = _DbContext.Locadoras.Where(m => m.NomeFantasia == filtro.NomeFantasia.RemoveDiacritics()).ToList();
            }

            return locadorasComNome;
        }


        public Locadora Update(LocadoraDTO dto)
        {
            var locadora = _DbContext.Locadoras.Find(dto.Id);
            if (locadora != null)
            {
                locadora.RazaoSocial= dto.RazaoSocial;
                locadora.NomeFantasia = dto.NomeFantasia;
                locadora.CNPJ = dto.CNPJ;
                locadora.Telefone= dto.Telefone;
                locadora.Email= dto.Email;
    
                _DbContext.Locadoras.Update(locadora);
                _DbContext.SaveChanges();

                var endereco = _DbContextEndereco.Enderecos.Where(x => x.LocadoraId == locadora.Id).FirstOrDefault();
                if (endereco != null)
                {
                    endereco.CEP = dto.Cep;
                    endereco.Rua = dto.Rua;
                    endereco.Numero = dto.Numero;
                    endereco.Bairro = dto.Bairro;
                    endereco.Estado = dto.Estado;
                    endereco.Cidade = dto.Cidade;

                    _DbContextEndereco.SaveChanges();
                }
            }

            return locadora ?? throw new InvalidOperationException("Locadora não encontrada");
        }




        public Locadora Delete(int id)
        {
            var locadora = _DbContext.Locadoras.Find(id);

            if (locadora != null)
            {
                var endereco = _DbContextEndereco.Enderecos.Where(x => x.LocadoraId.Equals(locadora.Id)).FirstOrDefault();
                if (endereco != null)
                {
                    _DbContextEndereco.Enderecos.Remove(endereco);
                    _DbContextEndereco.SaveChanges();
                }

                _DbContext.Locadoras.Remove(locadora);
                _DbContext.SaveChanges();
            }

            return locadora ?? throw new InvalidOperationException("Locadora não encontrada");
        }

        public Locadora Create(LocadoraDTO dto)
        {
            Locadora locadora = new Locadora()
            {
                NomeFantasia = dto.NomeFantasia,
                CNPJ = dto.CNPJ,
                Email = dto.Email,
                Telefone = dto.Telefone,
                RazaoSocial = dto.RazaoSocial,
            };

            _DbContext.Add(locadora);
            _DbContext.SaveChanges(true);

            Endereco endereco = new Endereco()
            {
                CEP = dto.Cep,
                Numero = dto.Numero,
                Rua = dto.Rua,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                LocadoraId = locadora.Id
            };

            _DbContextEndereco.Add(endereco);
            _DbContextEndereco.SaveChanges();

            return locadora;
        }


        public Locadora Update(Locadora entity)
        {
            throw new NotImplementedException();
        }

        public Locadora Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
