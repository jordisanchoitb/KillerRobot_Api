﻿using KillerRobot_Api.Data;
using KillerRobot_Api.Models;
using KillerRobot_Api.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KillerRobot_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDTO _response; 

        public ScoresController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDTO();
        }

        [HttpGet("GetScores")]
        public ResponseDTO GetScores()
        {
            try
            {
                IEnumerable<Scores> scores = _context.Scores.ToList();
                _response.Data = scores;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("GetScores/{id}")]
        public ResponseDTO GetScores(int id)
        {
            try
            {
                Scores scores = _context.Scores.FirstOrDefault(x => x.Id == id);
                _response.Data = scores;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost("PostScores")]
        public ResponseDTO GetScores([FromBody] Scores scores)
        {
            try
            {
                _context.Scores.Add(scores);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut("PutScores")]
        public ResponseDTO PutStudent([FromBody] Scores scores)
        {
            try
            {
                _context.Scores.Update(scores);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("DeleteStudent/{id}")]
        public ResponseDTO DeleteStudent(int id)
        {
            try
            {
                Scores scores = _context.Scores.FirstOrDefault(x => x.Id == id); // Obtenim l'element amb l'Id indicat
                _context.Scores.Remove(scores);
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