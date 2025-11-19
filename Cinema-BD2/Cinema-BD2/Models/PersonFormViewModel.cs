using System.Collections.Generic;

namespace Cinema_BD2.Models
{
    public class PersonFormViewModel
    {
        public Person Person { get; set; } = new Person();

        // Lista de roles disponíveis (para exibir os checkboxes)
        public List<Role>? Roles { get; set; }

        // Armazena os IDs dos roles selecionados no formulário
        public List<int>? SelectedRoles { get; set; }

        // Lista para popular o DropDown de Gender
        public List<Gender>? Genders { get; set; }

        // Lista para popular o DropDown de Address
        public List<Address>? Addresses { get; set; }
    }
}
