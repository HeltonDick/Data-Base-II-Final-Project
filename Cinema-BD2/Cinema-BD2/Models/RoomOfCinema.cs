using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_BD2.Models
{
    public class RoomOfCinema
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "é necessario que a sala sejá incerida")]
        [Display(Name = "Sala")]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        [Required(ErrorMessage = "é necessario que o cinema sejá incerido")]
        [Display(Name = "Cinema")]
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        
        
    }
}
