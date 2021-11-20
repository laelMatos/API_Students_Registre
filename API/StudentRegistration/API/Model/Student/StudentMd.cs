using Common;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class StudentRA
    {
        /// <summary>
        /// Código de identificação do aluno
        /// </summary>
        [Required(ErrorMessage = "O RA é um campo obrigatório")]
        [MaxLength(20, ErrorMessage = "O RA pode conter no máximo 140 caracteres")]
        [MinLength(2, ErrorMessage = "O RA deve conter no minimo 02 caracteres")]
        public string RA { get; set; }
    }
   
    public class StudentUpdateMd : StudentRA
    {
        /// <summary>
        /// Nome do aluno
        /// </summary>
        [MaxLength(60, ErrorMessage = "O nome pode conter no máximo 140 caracteres")]
        [MinLength(2, ErrorMessage = "O nome deve conter no minimo 02 caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Email do aluno
        /// </summary>
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(60, ErrorMessage = "O email pode conter no máximo 140 caracteres")]
        public string Email { get; set; }
    }

    public class StudentMd : StudentRA
    {

        /// <summary>
        /// Documento de identificação do aluno
        /// </summary>
        [Required(ErrorMessage ="O CPF é um campo obrigatóro")]
        [MinLength(11, ErrorMessage = "O CPF deve possuir ao menos 11 números")]
        [MaxLength(14, ErrorMessage = "O CPF deve possuir no máximo 14 caracteres")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido.")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome do aluno
        /// </summary>
        [Required(ErrorMessage = "O nome é um campo obrigatório")]
        [MaxLength(60, ErrorMessage ="O nome pode conter no máximo 140 caracteres")]
        [MinLength(2, ErrorMessage ="O nome deve conter no minimo 02 caracteres")]
        public string Name { get; set; }

        /// <summary>
        /// Email do aluno
        /// </summary>
        [EmailAddress(ErrorMessage ="Email inválido")]
        [Required(ErrorMessage = "O email é um campo obrigatório")]
        [MaxLength(60, ErrorMessage ="O email pode conter no máximo 140 caracteres")]
        public string Email { get; set; }
    }
}
