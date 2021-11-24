using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Login é um campo obrigatório")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha é um campo obrigatório")]
        public string Password { get; set; }
    }
}
