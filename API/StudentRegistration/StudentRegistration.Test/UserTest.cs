using StudentRegistration.Domain;
using StudentRegistration.Domain.Enuns;
using Xunit;

namespace StudentRegistration.Test
{
    public class UserTest
    {
        [Theory]
        [InlineData(null, null, null, ETypeUser.Adm)]
        [InlineData("", "teste@gmail.com", "12345678", ETypeUser.Adm)]
        [InlineData("lael santos de matos", "", "12345678", ETypeUser.Adm)]
        [InlineData("lael santos de matos", "teste@gmail.com", "", ETypeUser.Adm)]
        [InlineData("lael santos de matos", "teste@gmail.com", "12", ETypeUser.Adm)]
        [InlineData("lael santos de matos", "teste@gmail", "12345678", ETypeUser.Adm)]
        public void CreateInvalidUser(string name, string email, string password, ETypeUser eType)
        {
            var user = new User(name, email, password, eType);

            Assert.False(user.NOTIFICATION.Success);
        }

        [Theory]
        [InlineData("lael santos de matos", "teste@gmail.com", "12345678", ETypeUser.Adm)]
        public void CreateValidUser(string name, string email, string password, ETypeUser eType)
        {
            var user = new User(name, email, password, eType);

            Assert.True(user.NOTIFICATION.Success);
        }

        [Fact]
        public void LastAcessUser()
        {
            var user = new User("lael santos de matos", "teste@gmail.com", "12345678", ETypeUser.Adm);
            user.SetLastAcess();

            Assert.NotNull(user.Last_acess);
        }

        [Theory]
        [InlineData(false)]
        public void ExistEmailUser(bool exist)
        {
            var user = new User("lael santos de matos", "teste@gmail.com", "12345678", ETypeUser.Adm);

            user.ExistEmail(exist);

            Assert.True(user.NOTIFICATION.Success);
        }

        [Fact]
        public string ValidToStringUser()
        {
            var user = new User(null, null, null, ETypeUser.Adm);

            return user.NOTIFICATION.ToString();
        }

    }
}
