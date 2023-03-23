using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddUserDTO
    {
        [Required(ErrorMessage = "Name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Name can not be longer than {1} characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required."), MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must contain 10 digits only")]
        public string MobileNo { get; set; }


        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        [MaxLength(64, ErrorMessage = "Email address can not be longer than {1} characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password should be minimum {2} to maximum {1} character long")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
