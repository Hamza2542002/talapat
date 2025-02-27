using System.ComponentModel.DataAnnotations;

namespace Talabat.Dtos
{
    public class RegisterDTO
    {
        [Required,MaxLength(100)]
        public string? DisplayName { get; set; }

        [Required,MaxLength(100)]
        public string? UserName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required,EmailAddress]
        public string? Email { get; set; }

        [Required,DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required,DataType(DataType.Password),Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}
