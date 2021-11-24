using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Domain
{
    public abstract class Pass : EmailAssets
    {

        protected Pass()
        {
        }

        public Pass(string password, string email):base(email)
        {
            SetPassword(password);
        }

        [Required]
        [StringLength(200)]
        public string Password { get; protected set; }


        /// <summary>
        /// Insere a senha
        /// </summary>
        /// <param name="password">Senha</param>
        public bool SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                MESSAGES.Add(new Messages() { Message = "Senha inválida", ErrorField= "Password" });
                return false;
            }

            this.Password = BCrypt.Net.BCrypt.HashPassword(password);
            return true;
        }
    }
}
