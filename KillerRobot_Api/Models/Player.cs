using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KillerRobot_Api.Models
{
    public class Player
    {
        [Key]
        public string name { get; set; }
        [InverseProperty("Player")]
        public IEnumerable<Scores> Scores { get; set; }
    }
}
