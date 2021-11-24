using StudentRegistration.Domain;
using Xunit;

namespace StudentRegistration.Test
{
    public class StudentTest
    {
        [Theory]
        [InlineData("d644cf6", "34523545657", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "lael santos de matos sdfsbsdfbsdfvvfsdsdfvsdvsdfvsdvsdfvsdsfgtredfg", "teste@gmail.com")]
        [InlineData("", "01257139169", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "l", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "lael santos de matos", "teste@gmail")]
        public void CreateInvalidStudents(string ra, string cpf, string name, string email)
        {
            var student = new Student(ra, cpf, name, email);

            Assert.False(student.NOTIFICATION.Success);
        }

        [Theory]
        [InlineData("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com")]
        public void CreateValidStudent(string ra, string cpf, string name, string email)
        {
            var student = new Student(ra, cpf, name, email);

            Assert.True(student.NOTIFICATION.Success);
        }

        [Theory]
        [InlineData("lael santos de matos sdfsbsdfbsdfvvfsdsdfvsdvsdfvsdvsdfvsdsfgtredfg", "teste@gmail.com")]
        [InlineData("l", "teste@gmail.com")]
        [InlineData("lael santos de matos", "teste@gmail")]
        public void UpdateInvalidStudent(string name, string email)
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");
            student.Update(name, email);

            Assert.False(student.NOTIFICATION.Success);
        }

        [Theory]
        [InlineData("lael santos de matos", "teste@gmail.com")]
        [InlineData("lael santos de matos", "")]
        [InlineData("", "teste@gmail.com")]
        public void UpdateValidStudent(string name, string email)
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");
            student.Update(name, email);

            Assert.True(student.NOTIFICATION.Success);
        }


        [Fact]
        public void CreateInvalidStudent()
        {
            var user = new Student(EHttpResponseCode.NotFound,
                        "Teste",
                        "Criando usuário enválido",
                        "RA");

            Assert.False(user.NOTIFICATION.Success);
        }
    }
}
