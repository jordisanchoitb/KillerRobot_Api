using System.ComponentModel.DataAnnotations;

namespace KillerRobot_Api.Models
{
    public class Player
    {
        [Key]
        public string name { get; set; }
    }
}
