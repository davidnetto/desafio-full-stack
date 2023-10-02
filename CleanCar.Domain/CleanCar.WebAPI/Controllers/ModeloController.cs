using CleanCar.Application;
using CleanCar.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        public readonly IModeloService _service;
        public readonly IMontadoraService _serviceMontatora;
        public readonly ILocadoraService _serviceLocadora;
        public readonly IVeiculoService _serviceVeiculo;
        public ModeloController(IModeloService service, IMontadoraService serviceMontatora, ILocadoraService serviceLocadora, IVeiculoService serviceVeiculo)
        {
            _service = service;
            _serviceMontatora = serviceMontatora;
            _serviceLocadora = serviceLocadora;
            _serviceVeiculo = serviceVeiculo;

        }

        [HttpGet("{id}")]
        public ActionResult<Modelo> GetById(int id)
        {
            var modelo = _service.Get(id);

            if (modelo == null)
            {
                return NotFound(); 
            }

            return Ok(modelo);
        }

        [HttpGet]
        public ActionResult<List<ModeloDTO>> Get()
        {
            var modelosFromService = _service.GetAll();
            var modeloDtoLista = new List<ModeloDTO>();

            var montadoras = _serviceMontatora.GetAll().ToDictionary(m => m.ID);

            foreach (var item in modelosFromService)
            {
                if (montadoras.TryGetValue(item.MontadoraId, out var montadora))
                {
                    var dto = new ModeloDTO();
                    dto.Nome = item.Nome;
                    dto.Id = item.ID;
                    dto.NomeMontadora = montadora.Nome;

                    modeloDtoLista.Add(dto);
                }
            }

            return Ok(modeloDtoLista);
        }


        [HttpPost]
        public ActionResult<Modelo> Post(Modelo modelo)
        {
            var Modelo = _service.Create(modelo);
            return Ok(Modelo);
        }

        [HttpPut]
        public ActionResult<Modelo> Put(Modelo montatora)
        {
            _service.Update(montatora);
            return Ok(montatora);
        }

        [HttpDelete]
        public ActionResult<Modelo> Delete(int id)
        {
            _service.Delete(id);
            return Ok(id);
        }
    }
}
