using CleanCar.Application;
using CleanCar.Domain;
using CleanCar.Domain.DTOs.Veiculo;
using CleanCar.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocadoraController : ControllerBase
    {
        public readonly ILocadoraService _service;
        public LocadoraController(ILocadoraService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<LocadoraDTO> GetById(int id)
        {
            LocadoraDTO dto = new LocadoraDTO() { Id = id};
            var locadora = _service.Get(dto);

            if (locadora == null)
            {
                return NotFound(); // Retorna um status 404 Not Found se a locadora não for encontrada
            }

            return Ok(locadora);
        }

        [HttpGet]
        public ActionResult<List<Locadora>> Get()
        {
            var locadorasFromService = _service.GetAll();
            return Ok(locadorasFromService);
        }

        [HttpGet("relatorio")]
        public ActionResult<List<ModeloDTO>> GetRelatorio()
        {
            var locadorasFromService = _service.GetRelatorio();
            return Ok(locadorasFromService);
        }

        [HttpPost]
        public ActionResult<Locadora> Post(LocadoraDTO locadoraDto)
        {
            var Locadora = _service.Create(locadoraDto);
            return Ok(Locadora);
        }


        [HttpPut]
        public ActionResult<Locadora> Put(LocadoraDTO locadora)
        {
            _service.Update(locadora);
            return Ok(locadora);
        }

        [HttpDelete]
        public ActionResult<Locadora> Delete(int id)
        {
            _service.Delete(id);
            return Ok(id);
        }
    }
}
