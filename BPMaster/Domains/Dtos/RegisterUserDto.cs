using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public  required string LastName { get; set; }

        public static RegisterUserDto Mock()
        {
            return new () {
                Username = "user@test.com",
                Password = "password",
                FirstName = "Test First Name",
                LastName = "Test Last Name"
            };
        }
    }
}
