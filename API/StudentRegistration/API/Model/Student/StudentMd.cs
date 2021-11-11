using SweetPet.Common;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class StudentMd
    {
        [Required(ErrorMessage ="O RA é um campo obrigatório")]
        public string RA { get; set; }

        [Required(ErrorMessage ="O CPF é um campo obrigatóro")]
        [MinLength(11, ErrorMessage = "O CPF deve possuir ao menos 11 números")]
        [MaxLength(14, ErrorMessage = "O CPF deve possuir no máximo 14 caracteres")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O nome é um campo obrigatório")]
        [MaxLength(140, ErrorMessage ="O nome pode conter no máximo 140 caracteres")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage ="Email inválido")]
        [Required(ErrorMessage = "O email é um campo obrigatório")]
        [MaxLength(140, ErrorMessage ="O email pode conter no máximo 140 caracteres")]
        public string Email { get; set; }
    }
}
