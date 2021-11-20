using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;

namespace StudentRegistration.Domain
{
    /// <summary>
    /// Entidade estudante
    /// </summary>
    [Table("Students")]
    public class Student
    {
        [NotMapped]
        public Notification NOTIFICATION { get; set; }
        [NotMapped]
        private List<Messages> MESSAGES;

        protected Student(){
            MESSAGES = new List<Messages>();
            NOTIFICATION = new Notification();
        }

        /// <param name="ra">Código de identificação do aluno</param>
        /// <param name="cpf">Documento de identificação do aluno</param>
        /// <param name="name">Nome do aluno</param>
        /// <param name="email">Email do aluno</param>
        public Student(string ra, string cpf, string name, string email)
        {

            RA = ra;
            CPF = cpf;
            Name = name;
            Email = email;
            Created_at = DateTime.UtcNow;
            
            Validate();
        }

        [Key]
        [Required]
        [MaxLength(20)]
        public string RA { get; protected set; }

        [Required]
        [MaxLength(14)]
        public string CPF { get; protected set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; protected set; }

        [Required]
        [MaxLength(60)]
        public string Email { get; protected set; }

        [Required]
        public DateTime Created_at { get; protected set; }
        public DateTime? Update_at { get; protected set; }
        public DateTime? Last_acess { get; protected set; }

        public void NewAccess()
        {
            Last_acess = DateTime.UtcNow;
        }

        /// <summary>
        /// Atualiza Nome ou/e Email
        /// </summary>
        public void Update(string? name, string? email)
        {
            NOTIFICATION = new Notification() { Title = "Atualização de dados" };
            MESSAGES = new List<Messages>();

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
            {
                MESSAGES.Add(new Messages() { Message = "Nenhum dado foi enviado, nome e email estão vazios" });
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    ValidName(name);
                    Name = name;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    ValidEmail(email);
                    Email = email;
                }


                Update_at = DateTime.UtcNow;
            }

            if (MESSAGES.Count > 0)
                NOTIFICATION.Messages = MESSAGES;
        }

        public void Validate()
        {
            NOTIFICATION = new Notification() { Title = "Validação dados do aluno" };
            MESSAGES = new List<Messages>();

            ValidRA(RA);
            ValidCPF(CPF);
            ValidName(Name);
            ValidEmail(Email);

            if (MESSAGES.Count > 0)
                NOTIFICATION.Messages = MESSAGES;
        }

        private bool ValidRA(string ra)
        {
            if(string.IsNullOrEmpty(ra) || ra.Length < 2 || ra.Length > 140)
            {
                MESSAGES.Add(new Messages() { Message = "RA inválido", ErrorField = "RA" });
                return false;
            }
            return true;
        }

        private bool ValidCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf) || !AssertionConcern.ValidaCPF(cpf))
            {
                MESSAGES.Add(new Messages() { Message = "Cpf inválido", ErrorField="CPF" });
                return false;
            }
            return true;
        }

        private bool ValidName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2 || name.Length > 60)
            {
                MESSAGES.Add(new Messages() { Message = "Nome inválido" });
                return false;
            }
            return true;
        }

        private bool ValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !AssertionConcern.AssertEmailIsValid(email))
            {
                MESSAGES.Add(new Messages() { Message = "Email inválido" });
                return false;
            }
            return true;

        }

    }
}
