using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoriosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLaboratorios()
        {
            var laboratorios = SpaceModel.Spaces.Where(s => s.Type == "Laboratorio").ToList();
            return Ok(laboratorios);
        }
    }
}