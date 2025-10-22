using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAulas()
        {
            var aulas = SpaceModel.Spaces.Where(s => s.Type == "Aula").ToList();
            return Ok(aulas);
        }
    }
}