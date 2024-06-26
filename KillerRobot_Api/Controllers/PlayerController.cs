﻿using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using KillerRobot_Api.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    [EnableCors(Program.CORSPolicyName)]
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
        [HttpPost("ShowPlayerScores")]
        public ResponseDTO ShowScores([FromBody] Player player)
        {
            try
            {
                if (GetLogin(player).IsSuccess)
                {
                    IEnumerable<Scores> scores = _context.Scores.ToList().Where(sc=>sc.PlayerName==player.Name);
                    foreach(Scores score in scores)
                    {
                        score.Player = null;
                    }
                    _response.Data = scores;
                }
            }catch(Exception ex)
            {
                _response.IsSuccess=false;
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
        public ResponseDTO UpdatePlayer([FromBody] PlayerChangeRequest changeRequest)
        {
            try
            {
                
                if (GetLogin(changeRequest.playerCheck).IsSuccess)
                {
                    Player trackedPlayer = _context.Players.FirstOrDefault(x=>x.Name==changeRequest.playerCheck.Name);
                    trackedPlayer.Password = Hasher.SHA256Hashing(changeRequest.newPassword).ToUpper();
                    _context.Players.Update(trackedPlayer);
                    _context.SaveChanges();
                }
                
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
