using KillerRobot_Api.Data;
using KillerRobot_Api.Models.DTO;
using KillerRobot_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response;

        public PlayerController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        [HttpGet("GetPlayer")]
        public ResponseDTO GetPlayer()
        {
            try
            {
                IEnumerable<Player> player = _context.Players.ToList();
                _response.Data = player;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("GetPlayer/{name}")]
        public ResponseDTO GetPlayer(string name)
        {
            try
            {
                Player player = _context.Players.FirstOrDefault(x => x.name == name);
                _response.Data = player;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost("PostPlayer")]
        public ResponseDTO PostPlayer([FromBody] Player players)
        {
            try
            {
                _context.Players.Add(players);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("PutPlayer")]
        public ResponseDTO PutStudent([FromBody] Player player)
        {
            try
            {
                _context.Players.Update(player);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeletePlayer/{name}")]
        public ResponseDTO DeletePlayer(string name)
        {
            try
            {
                Player player = _context.Players.FirstOrDefault(x => x.name == name); // Obtenim l'element amb l'Id indicat
                _context.Players.Remove(player);
                _context.SaveChanges();
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
