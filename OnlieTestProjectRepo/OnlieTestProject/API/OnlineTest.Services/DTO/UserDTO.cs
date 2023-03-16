using OnlineTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Services.DTO
{
    public class UserDTO
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


        [Required(ErrorMessage = "Password is required")]
        [MaxLength(256)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
