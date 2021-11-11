using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegistration.Domain
{
    /// <summary>
    /// Entidade estudante
    /// </summary>
    [Table("Students")]
    public class Student
    {
        /// <param name="ra">Código de identificação do aluno</param>
        /// <param name="cpf">Documento de identificação do aluno</param>
        /// <param name="name">Nome do aluno</param>
        public Student(string ra, string cpf, string name, string email)
        {
            RA = ra;
            CPF = cpf;
            Name = name;
            Email = email;
        }

        [Key]
        [Required]
        public string RA { get; protected set; }

        [Required]
        [MaxLength(14)]
        public string CPF { get; protected set; }

        [Required]
        [MaxLength(140)]
        public string Name { get; set; }

        [Required]
        [MaxLength(140)]
        public string Email { get; set; }

        public void Validate()
        {

        }

    }
}
