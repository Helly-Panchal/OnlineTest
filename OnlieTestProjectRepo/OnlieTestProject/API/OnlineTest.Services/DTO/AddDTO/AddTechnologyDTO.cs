using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddTechnologyDTO
    {

        [Required(ErrorMessage = "Technology name is required. Please Enter Name")]
        [MaxLength(20, ErrorMessage = "Technology name can not be longer than {1} characters.")]
        public string TechName { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
