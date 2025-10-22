using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanchasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCanchas()
        {
            var canchas = SpaceModel.Spaces.Where(s => s.Type == "Cancha").ToList();
            return Ok(canchas);
        }
    }
}