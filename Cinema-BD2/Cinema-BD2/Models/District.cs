using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome do Bairro")]
        [StringLength(100)]
        public string? Name { get; set; }
        public ICollection<Address>? Addresses  { get; set; }
    }
}
