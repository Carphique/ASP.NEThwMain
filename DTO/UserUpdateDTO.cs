using System.ComponentModel.DataAnnotations;

namespace ASP.NEThwMain.DTO
{
    public class UserUpdateDTO
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? LastName { get; set; }
    }
}
