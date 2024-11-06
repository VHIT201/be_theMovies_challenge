using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class InfomationDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Role { get; set; }
    }
}
