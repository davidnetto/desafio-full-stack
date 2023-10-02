using CleanCar.Application;
using CleanCar.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontadoraController : ControllerBase
    {
        public readonly IMontadoraService _service;
        public MontadoraController(IMontadoraService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Montadora> GetById(int id)
        {
            var montadora = _service.Get(id);

            if (montadora == null)
            {
                return NotFound(); // Retorna um status 404 Not Found se a montadora não for encontrada
            }

            return Ok(montadora);
        }

        [HttpGet]
        public ActionResult<List<Montadora>> Get() 
        {
            var montadorasFromService = _service.GetAll();
            return Ok(montadorasFromService);
        }

        [HttpPost]
        public ActionResult<Montadora> Post(Montadora montadora)
        {
            var Montadora = _service.Create(montadora);
            return Ok(Montadora);
        }

        [HttpPut]
        public ActionResult<Montadora> Put(Montadora montatora)
        {
            _service.Update(montatora);
            return Ok(montatora);
        }

        [HttpDelete]
        public ActionResult<Montadora> Delete(int id)
        {
            _service.Delete(id);
            return Ok(id);
        }
    }
}
