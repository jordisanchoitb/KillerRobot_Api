using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillerRobot_Api.Models
{
    public class Player
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
