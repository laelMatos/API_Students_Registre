using StudentRegistration.Domain;

namespace StudentRegistration.Service
{
    public class AuthService : IAuthService
    {
        private IUserRepository _userRpos;
        public AuthService(IUserRepository userRepository)
        {
            _userRpos = userRepository;
        }

        public User ValidAuthentication(string login, string password)
        {
            var userDb = _userRpos.GetByLogin(login);

            if (userDb == null)
            {
                return null;
            }

            userDb.PasswordIsValid(password);

            return userDb;
        }
    }

}
