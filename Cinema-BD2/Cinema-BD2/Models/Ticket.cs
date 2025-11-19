using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_BD2.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DatePurchase { get; set; }
        public int Price { get; set; }

        [Required(ErrorMessage = "Selecione uma pessoa para o ingresso.")]
        [Display(Name = "Pessoa")]
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person? Person { get; set; }

        [Required(ErrorMessage = "Selecione uma sessao para o ingresso.")]
        [Display(Name = "sessao")]
        public int SessionId { get; set; }
        [ForeignKey("SessionId")]
        public Session? Session { get; set; }
    }
}
