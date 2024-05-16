using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : Controller
    {
        private readonly AppDbContext _context; // per realitzar la injecció de dependències de la base de dades
        private ResponseDTO _response; // Resposta a les peticions

        public ScoresController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        [HttpGet("GetScores")] // Indiquem que aquest mètode respon a peticions GET, amb la ruta GetStudents
        public ResponseDTO GetScores()
        {
            try
            {
                IEnumerable<Scores> scores = _context.Scores.ToList(); // Obtenim tots els elements de la taula Students
                _response.Data = scores; // Afegim els elements a la resposta
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
