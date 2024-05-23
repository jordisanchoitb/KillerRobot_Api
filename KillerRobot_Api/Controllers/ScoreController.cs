using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    public class ScoreController : Controller
    {
        private readonly AppDbContext _context; // per realitzar la injecció de dependències de la base de dades
        private ResponseDTO _response; // Resposta a les peticions

        // Constructor, fem la injecció de dependències de la base de dades
        public ScoreController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }
        [HttpGet("GetScore/{id}")]
        [EnableCors]
        public ResponseDTO GetScore(int id)
        {
            try
            {
                _response.Data = _context.Scores.ToList().FirstOrDefault(x=>x.Id==id);

            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("GetScore")]
        [EnableCors]
        public ResponseDTO GetScore()
        {
            try
            {
                _response.Data = _context.Scores.ToList();
            }catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost("PostScore")]
        public ResponseDTO PostScore([FromBody] Scores score)
        {
            try
            {
                score.Player = null;
                _context.Scores.Add(score);
                _context.SaveChanges();
            }catch(Exception ex ) 
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("ChangeScore")]
        public ResponseDTO ChangeScore([FromBody] Scores score)
        {
            try
            {
                score.Player = null;
                _context.Scores.Update(score);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeleteScore")]
        public ResponseDTO DeleteScore([FromBody] Scores score)
        {
            try
            {
                score.Player = null;
                _context.Scores.Remove(score);
                _context.SaveChanges();
            }catch( Exception ex )
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeleteScore/{id}")]
        public ResponseDTO DeleteScore(int id)
        {
            try
            {
                Scores score = _context.Scores.FirstOrDefault(x=>x.Id==id);
                _context.Scores.Remove(score);
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
