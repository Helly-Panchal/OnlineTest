using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.GetDTO
{
    public class GetTechnologyDTO
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Technology name is required. Please Enter Name")]
        [MaxLength(50, ErrorMessage = "Technology name can not be longer than 50 characters.")]
        public string TechName { get; set; }
    }
}
