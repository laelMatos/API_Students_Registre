using System.Collections.Generic;


namespace StudentRegistration.Domain
{
    public interface IStudentRepository
    {
        /// <summary>
        /// Insere novos alunos
        /// </summary>
        /// <param name="student">Dados do aluno a ser inserido</param>
        /// <returns>Dados do aluno inserido</returns>
        Student InsertStudent(Student student);
        /// <summary>
        /// Atualiza os dados do aluno
        /// </summary>
        /// <param name="student">Dados do aluno</param>
        /// <returns>Dados atualizados</returns>
        Student Update(Student student);
        /// <summary>
        /// Remove o aluno definido
        /// </summary>
        /// <param name="student">Dados do aluno</param>
        /// <returns>Confirmação da ação</returns>
        bool Delete(Student student);
        /// <summary>
        /// Busaca todos os alunos cadastrados
        /// </summary>
        /// <returns>Todos os alunos encontrados</returns>
        IEnumerable<Student> GetAllStudents();
    }
}
