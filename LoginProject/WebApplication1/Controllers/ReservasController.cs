using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetSpace(int id)
        {
            var space = SpaceModel.GetSpaceById(id);
            if (space == null)
                return NotFound(new { message = "Space not found" });
            return Ok(space);
        }

        [HttpPost("reservar")]
        public IActionResult Reservar(int idUsuario, int idEspacio, DateTime diaReserva)
        {
            Console.WriteLine($"Received reservation request - idUsuario: {idUsuario}, idEspacio: {idEspacio}, diaReserva: {diaReserva}"); // Debug
            var reserva = ReservaModel.AddReserva(idUsuario, idEspacio, diaReserva);
            if (reserva == null)
                return BadRequest(new { message = "Reservation failed (e.g., space unavailable or past date)" });
            return Ok(new { message = "Reservation successful", reserva = reserva });
        }
    }
}