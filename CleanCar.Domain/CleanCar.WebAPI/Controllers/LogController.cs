using CleanCar.Application;
using CleanCar.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CleanCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        public readonly ILogService _service;
        public LogController(ILogService service)
        {
            _service = service;
        }
     
        [HttpGet]
        public ActionResult<List<Log>> Get() 
        {
            var logsFromService = _service.GetAll();
            return Ok(logsFromService);
        }
    }
}
