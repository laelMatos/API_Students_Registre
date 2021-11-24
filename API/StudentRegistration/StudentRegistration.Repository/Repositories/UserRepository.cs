using StudentRegistration.Domain;
using System.Linq;

namespace StudentRegistration.Repository
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(ConnectionEf dataContextMySql)
        {
            _Db = dataContextMySql;
        }

        public bool ExistEmail(string email)
        {
            return _Db.Users.Any(x=>x.Email == email);
        }

        public User GetByEmail(string email)
        {
            return _Db.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User GetByLogin(string login)
        {
            return _Db.Users.Where(x => x.Email == login).FirstOrDefault();
        }
    }
}
