using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.GetDTO
{
    public class GetUserDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Name can not be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required."), MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must contain 10 digits only")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        [MaxLength(64, ErrorMessage = "Email address can not be longer than 64 characters")]
        public string Email { get; set; }
        public bool IsActive { get; set; }

    }
}