using CleanCar.Application;
using CleanCar.Domain;
using CleanCar.Domain.DTOs.Veiculo;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        public readonly IVeiculoService _service;
        public VeiculoController(IVeiculoService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public ActionResult<Veiculo> GetById(int id)
        {
            var veiculo = _service.Get(id);

            if (veiculo == null)
            {
                return NotFound(); // Retorna um status 404 Not Found se a montadora não for encontrada
            }

            return Ok(veiculo);
        }

        [HttpGet]
        public ActionResult<List<Veiculo>> Get()
        {
            var veiculoFromService = _service.GetAll();
            return Ok(veiculoFromService);
        }

        [HttpGet("filter")]
        public ActionResult<List<VeiculoDTO>> GetByFilter([FromQuery] VeiculoDTO dto)
        {
            try
            {
                var veiculosFiltrados = _service.Get(dto);
                return Ok(veiculosFiltrados);
            }
            catch (Exception ex)
            {
                // Você pode lidar com exceções aqui e retornar uma resposta de erro apropriada.
                return StatusCode(500, "Ocorreu um erro ao buscar os veículos: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Veiculo> Post(Veiculo veiculo)
        {
            var Veiculo = _service.Create(veiculo);
            return Ok(Veiculo);
        }

        [HttpPut]
        public ActionResult<Veiculo> Put(Veiculo montatora)
        {
            _service.Update(montatora);
            return Ok(montatora);
        }

        [HttpDelete]
        public ActionResult<Veiculo> Delete(int id)
        {
            _service.Delete(id);
            return Ok(id);
        }
    }
}
