using StudentRegistration.Domain;

namespace StudentRegistration.Service
{
    /// <summary>
    /// Interface do serviço de usuário
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// Busca o usuario pelo email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Usuário</returns>
        User GetByEmail(string email);

        User Insert(User user);

        User Update(User user);

        /// <summary>
        /// Verifica se exite um usuário cadastrado com o mesmo email
        /// </summary>
        /// <param name="email">Email a ser verificado</param>
        /// <returns>Confirmação</returns>
        bool ExistEmail(string email);
        /// <summary>
        /// Busca o usuário pelo código de identificação
        /// </summary>
        /// <param name="id">Código de identificação</param>
        /// <returns>Usuário encontrado</returns>
        User GetById(int id);
    }
}
