using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Studio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome do estudio' é obrigatório.")]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
