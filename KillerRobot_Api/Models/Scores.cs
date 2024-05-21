using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillerRobot_Api.Models
{
    public class Scores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [ForeignKey("PlayerName")]
        public Player Player { get; set; }
        [Required]
        public int Score { get; set; }
        public int CompletionTime { get; set; }
        [Required]
        public string Level {  get; set; }
    }
}
