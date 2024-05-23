using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KillerRobot_Api.Models
{
    public class Scores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [ForeignKey("PlayerName")]
        [JsonIgnore()]
        public Player Player { get; set; }
        [Required]
        public int Score { get; set; }
        public int CompletionTime { get; set; }
        [Required]
        public string Level {  get; set; }
    }
}
