using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema_BD2.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }

        [Required(ErrorMessage = "Selecione uma sala.")]
        [Display(Name = "Sala")]
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        [Required(ErrorMessage = "Selecione um idioma.")]
        [Display(Name = "Idioma")]
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language? Language { get; set; }

        [Required(ErrorMessage = "Selecione um filme")]
        [Display(Name = "Filme")]
        public int FilmId { get; set; }
        [ForeignKey("FilmId")]
        public Film? Film { get; set; }

        [Required(ErrorMessage = "Selecione um Dimensão")]
        [Display(Name = "Dimensão")]
        public int DimensionId { get; set; }
        [ForeignKey("DimensionId")]
        public Dimension? Dimension { get; set; }


    }
}
