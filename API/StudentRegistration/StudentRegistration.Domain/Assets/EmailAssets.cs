using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Domain
{
    public abstract class EmailAssets
    {
        [NotMapped]
        public Notification NOTIFICATION { get; set; }
        [NotMapped]
        protected List<Messages> MESSAGES;

        protected EmailAssets()
        {
            MESSAGES = new List<Messages>();
            NOTIFICATION = new Notification();
        }

        protected EmailAssets(string email)
        {
            MESSAGES = new List<Messages>();
            NOTIFICATION = new Notification();

            Email = email;
        }

        [Required]
        [MaxLength(60)]
        public string Email { get; set; }

        protected bool ValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !AssertionConcern.AssertEmailIsValid(email))
            {
                MESSAGES.Add(new Messages() { Message = "Email inválido", ErrorField= "Email" });
                return false;
            }
            return true;

        }
    }
}
