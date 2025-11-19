using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Classification
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira Uma idade minima")]
        public int MinAge { get; set; }
    }
}
