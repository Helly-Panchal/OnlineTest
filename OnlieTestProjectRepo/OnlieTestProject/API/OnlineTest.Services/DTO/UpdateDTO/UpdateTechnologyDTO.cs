using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.UpdateDTO
{
    public class UpdateTechnologyDTO
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Technology name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Technology name can not be longer than 50 characters.")]
        public string TechName { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}
