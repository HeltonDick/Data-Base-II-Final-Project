using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Street
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da rua é obrigatório.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Address>? Addresses { get; set; }
    }
}
