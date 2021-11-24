using StudentRegistration.Domain;


namespace StudentRegistration.Service
{
    /// <summary>
    /// Serviço de autenticação do usuário
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="Login">Login do usuário (email||login)</param>
        /// <param name="Password">Senha do usuário</param>
        /// <returns>Usuario com credenciais correspondente</returns>
        public User ValidAuthentication(string Login, string Password);
    }
}
