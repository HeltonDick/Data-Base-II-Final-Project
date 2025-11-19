using System.ComponentModel.DataAnnotations;

namespace Cinema_BD2.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A duração é obrigatória.")]
        public int Duration { get; set; } // minutos

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        // Relacionamentos
        [Required(ErrorMessage = "Selecione uma classificação.")]
        public int ClassificationId { get; set; }
        public Classification? Classification { get; set; }

        public ICollection<Genre>? Genres { get; set; }
        public ICollection<Studio>? Studios { get; set; }
    }
}
