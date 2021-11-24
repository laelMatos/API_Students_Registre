namespace StudentRegistration.Domain
{
    /// <summary>
    /// Interface do repositorio de usuário
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Busca usuario por algum parametro passado
        /// </summary>
        /// <param name="login">Valor referente a Login || Email || CPF</param>
        /// <returns></returns>
        public User GetByLogin(string? login);
        bool ExistEmail(string email);

        /// <summary>
        /// Busca o usuario pelo email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Usuário</returns>
        User GetByEmail(string email);
    }
}
