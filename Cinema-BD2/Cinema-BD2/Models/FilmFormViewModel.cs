using System.ComponentModel.DataAnnotations;
using Cinema_BD2.Models;

namespace Cinema_BD2.ViewModels
{
    public class FilmFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A duração é obrigatória.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione uma classificação.")]
        public int ClassificationId { get; set; }

        public List<Genre>? Genres { get; set; }
        public List<int>? SelectedGenreIds { get; set; }

        public List<Studio>? Studios { get; set; }
        public List<int>? SelectedStudioIds { get; set; }

        public List<Classification>? Classifications { get; set; }
    }
}
