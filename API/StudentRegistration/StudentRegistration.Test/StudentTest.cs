using StudentRegistration.Domain;
using Xunit;

namespace StudentRegistration.Test
{
    public class StudentTest
    {
        [Theory]
        [InlineData("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com")]//ok
        [InlineData("d644cf6", "34523545657", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "lael santos de matos sdfsbsdfbsdfvvfsdsdfvsdvsdfvsdvsdfvsdsfgtredfg", "teste@gmail.com")]
        [InlineData("", "01257139169", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "", "lael santos de matos", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "l", "teste@gmail.com")]
        [InlineData("d644cf6", "01257139169", "lael santos de matos", "teste@gmail")]
        public void CreateStudent(string ra, string cpf, string name, string email)
        {
            var student = new Student(ra, cpf, name, email);

            Assert.True(student.NOTIFICATION.Success);
        }

        [Theory]
        [InlineData("lael santos de matos", "teste@gmail.com")]//ok
        [InlineData("lael santos de matos", "")]//ok
        [InlineData("lael santos de matos sdfsbsdfbsdfvvfsdsdfvsdvsdfvsdvsdfvsdsfgtredfg", "teste@gmail.com")]
        [InlineData("", "teste@gmail.com")]//ok
        [InlineData("l", "teste@gmail.com")]
        [InlineData("lael santos de matos", "teste@gmail")]
        public void UpdateStudent(string name, string email)
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");
            student.Update(name, email);

            Assert.True(student.NOTIFICATION.Success);
        }
        [Fact]
        public void UpdateStudent_2()
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");

            student.Update("José eduardo", null);//ok

            Assert.True(student.NOTIFICATION.Success);
        }

        [Fact]
        public void UpdateStudent_3()
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");

            student.Update(null, "teste@gmail.com");//ok

            Assert.True(student.NOTIFICATION.Success);
        }

        [Fact]
        public void UpdateStudent_4()
        {
            var student = new Student("d644cf6", "01257139169", "lael santos de matos", "teste@gmail.com");

            student.Update(null, null);

            Assert.True(student.NOTIFICATION.Success);
        }
    }
}
