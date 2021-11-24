using Common;
using StudentRegistration.Domain.Enuns;
using System.ComponentModel.DataAnnotations;

namespace API.Model.User
{
    public class UserRequest
    {
        /// <summary>
        /// Nome do usuário
        /// </summary>
        [Required(ErrorMessage = "Informe o nome do usuário")]
        [MaxLength(60)]
        public string Name { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(ErrorMessage = "Infome o email")]
        [MaxLength(60)]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required(ErrorMessage = "Informe a senha do usuário")]
        [StringLength(200)]
        public string Password { get; set; }

        /// <summary>
        /// Tipo do usuário
        /// </summary>
        [Required]
        [CustomValidationEnums(typeof(ETypeUser), ErrorMessage = "Tipo inválido")]
        public ETypeUser eType { get; set; }
    }
}
