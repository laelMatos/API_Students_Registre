using System.Collections.Generic;

namespace StudentRegistration.Domain
{
    public interface IStudentService
    {
        /// <summary>
        /// Busaca todos os alunos cadastrados
        /// </summary>
        /// <returns>Todos os alunos encontrados</returns>
        IEnumerable<Student> GetAll();
        /// <summary>
        /// Busca aluno pelo RA
        /// </summary>
        /// <param name="ra">Código de identificação do aluno</param>
        /// <returns></returns>
        Student GetByRA(string ra);
        /// <summary>
        /// Insere novos alunos
        /// </summary>
        /// <param name="student">Dados do aluno a ser inserido</param>
        /// <returns>Dados do aluno inserido</returns>
        Student Add(Student student);
        /// <summary>
        /// Atualiza os dados do aluno
        /// </summary>
        /// <param name="ra">Código de identificação do launo</param>
        /// <param name="email">Email do aluno</param>
        /// <param name="name">Nome do aluno</param>
        /// <returns>Dados atualizados</returns>
        Student Update(string ra, string email, string name);
        /// <summary>
        /// Remove o aluno definido
        /// </summary>
        /// <param ra="student">Código de identificação do aluno</param>
        /// <returns>Confirmação da ação</returns>
        Notification Delete(string ra);

    }
}
