using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Cinema_BD2.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "CPF inválido. Formato: 000.000.000-00")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Person), nameof(ValidateBirthDate))]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O número de contato é obrigatório.")]
        [Display(Name = "Contato")]
        [RegularExpression(@"^\+55\s\(\d{2}\)\s\d{4,5}\-\d{4}$", ErrorMessage = "Telefone inválido. Formato: +55 (49) 99999-9999")]
        public string Contact { get; set; } = string.Empty;

        // Relações
        [Required]
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }

        [Required]
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Role>? Roles { get; set; }


        // --- Validação de data ---
        public static ValidationResult? ValidateBirthDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
                return new ValidationResult("A data de nascimento não pode ser futura.");

            if ((DateTime.Now.Year - date.Year) < 1)
                return new ValidationResult("A pessoa deve ter pelo menos 1 ano de idade.");

            return ValidationResult.Success;
        }

        // --- Formatação automática ---
        public void FormatContact()
        {
            string digits = Regex.Replace(Contact, @"\D", "");
            if (digits.Length == 11)
            {
                Contact = $"+55 ({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7)}";
            }
        }
    }
}
