using System.ComponentModel.DataAnnotations;

namespace KillerRobot_Api.Models
{
    public class Scores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Player Player { get; set; }
        [Required]
        public int Score { get; set; }
        public float? CompletionTime { get; set; }
        [Required]
        public string Level { get; set; }
    }
}
