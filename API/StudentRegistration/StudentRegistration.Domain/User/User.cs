using Common;
using StudentRegistration.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistration.Domain
{
    public class User : Pass
    {
        protected User()
        {
        }

        public User(string name, string email, string password, ETypeUser type):base(password, email)
        {
            Name = name;
            Type = type;
            Created_at = DateTime.UtcNow;

            Validate();
        }

        [Key]
        public int Id { get; protected set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        public UInt16 EType { get; protected set; }

        [NotMapped]
        public ETypeUser Type
        {
            get { return (ETypeUser)EType; }
            set { EType = (UInt16)value; }
        }

        [Required]
        public DateTime Created_at { get; protected set; }
        public DateTime? Update_at { get; protected set; }
        public DateTime? Last_acess { get; protected set; }

        public void SetLastAcess()
        {
            Last_acess = DateTime.UtcNow;
        }
        public void SetUpdate()
        {
            Update_at = DateTime.UtcNow;
        }

        public void PasswordIsValid(string? password)
        {
            if (string.IsNullOrEmpty(password) || !BCrypt.Net.BCrypt.Verify(password, this.Password))
            {
                NOTIFICATION.Title = "Falha no login";
                MESSAGES.Add(new Messages() { Message = "Login ou senha inválidos!.", ErrorField = "" });
                NOTIFICATION.Messages = MESSAGES;
            }
        }

        public void Validate()
        {
            ValidName(Name);
            ValidEmail(Email);

            if (MESSAGES.Count > 0)
                NOTIFICATION.Messages = MESSAGES;
        }

        private bool ValidName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2 || name.Length > 60)
            {
                MESSAGES.Add(new Messages() { Message = "Nome inválido", ErrorField= "Name" });
                return false;
            }
            return true;
        }

        /// <summary>
        /// atribui uma mensagem de erro caso tenha um usuario como mesmo email.
        /// </summary>
        /// <param name="exist">Confirmação da existencia</param>
        public void ExistEmail(bool exist)
        {
            if (exist)
            {
                MESSAGES.Add(new Messages() { Message = "O email cadastrado ja tem um usuario utilizando", ErrorField="Email"});
                NOTIFICATION.Messages = MESSAGES;
            }
        }

    }
}
