using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddTechnologyDTO
    {

        [Required(ErrorMessage = "Technology name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Technology name can not be longer than 50 characters.")]
        public string TechName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int CreatedBy { get; set; }
    }
}
