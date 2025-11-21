using System.ComponentModel.DataAnnotations;
using Cinema_BD2.Models;

namespace Cinema_BD2.ViewModels
{
    public class FilmFormViewModel
    {
        // Puxamo Film de uma vez so pra num precisa fica reconstruino daq tlgd?
        public Film Film { get; set; } = new Film();

        // Lista de Generos disponíveis (para exibir os checkboxes)
        public List<Genre>? Genres { get; set; }
        public List<int>? SelectedGenreIds { get; set; }

        // Lista de Estudios disponíveis (para exibir os checkboxes)
        public List<Studio>? Studios { get; set; }
        public List<int>? SelectedStudioIds { get; set; }

        public List<Classification>? Classifications { get; set; }

        // OOOOOO ANIMAL DE DIVERSAS MAMAS, PQ QUE AQUI EU CONSIDERO O ID E EM PERSON NAO????
        // PQ EM PERSON EU TENHO SO UM SEXO, SO UM ENDERECO E VARIOS CARGOS
        // OS QUE SAO VARIOS EU COLOCO EM UMA VARIAVEL QUE GUARDE QUEM É QUEM SACO PUTINHA?
        // USAMO ID QUANDO FOR CELECIONA MAIS DE UM ITEN
    }

}
