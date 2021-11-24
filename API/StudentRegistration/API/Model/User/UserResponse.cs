using StudentRegistration.Domain.Enuns;

namespace API.Model
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ETypeUser Type { get; set; }
    }
}
