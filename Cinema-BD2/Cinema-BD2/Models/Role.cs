using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Cargo' é obrigatório.")]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
