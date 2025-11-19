using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Dimension
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira Um Nome para dimenção")]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
