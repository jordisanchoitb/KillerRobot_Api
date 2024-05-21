using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using KillerRobot_Api.Utils;
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
                Player CheckPlayer = _context.Players.FirstOrDefault(x => x.Name == player.Name);
                string recPlayerPass = Hasher.SHA256Hashing(player.Password).ToUpper();
                _response.IsSuccess = recPlayerPass==CheckPlayer.Password;
                if(!_response.IsSuccess) 
                {
                    _response.Message = "Incorrect login, check user and password";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost("RegisterPlayer")]
        public ResponseDTO RegisterPlayer([FromBody] Player player)
        {
            try
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("ChangePassword")]
        public ResponseDTO UpdatePlayer([FromBody] Player player)
        {
            try
            {
                _context.Players.Update(player);
                _context.SaveChanges();
            }catch(Exception ex )
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeleteUser")]
        public ResponseDTO DeleteUser([FromBody] Player player)
        {
            try
            {
                if(GetLogin(player).IsSuccess)
                {
                    Player remove = _context.Players.FirstOrDefault(x=>x.Name==player.Name);
                    _context.Players.Remove(remove);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("You can't delete this account");
                }
            }catch( Exception ex )
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
