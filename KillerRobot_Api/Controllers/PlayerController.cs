using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context; // per realitzar la injecció de dependències de la base de dades
        private ResponseDTO _response; // Resposta a les peticions

        // Constructor, fem la injecció de dependències de la base de dades
        public PlayerController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }
        [HttpPost("CheckLogin")]
        public ResponseDTO GetLogin([FromBody] Player player)
        {
            try
            {
                Player CheckPlayer = _context.Players.FirstOrDefault(x => x.Name == player.Name); // Obtenim tots els elements de la taula Students
                 // Afegim els elements a la resposta
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
