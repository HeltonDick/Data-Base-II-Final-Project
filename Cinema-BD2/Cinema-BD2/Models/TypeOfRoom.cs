using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class TypeOfRoom
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Descrição' é obrigatório.")]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
