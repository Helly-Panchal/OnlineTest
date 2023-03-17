using System.ComponentModel.DataAnnotations;
using Castle.Components.DictionaryAdapter;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddTestDTO
    {
        public int TechnologyId { get; set; }

        [Required(ErrorMessage = "Test name is required. Please Enter Test name")]
        [MaxLength(50, ErrorMessage = "Test name can not be longer than {1} characters.")]
        public string TestName { get; set; }


        [Required(ErrorMessage = "Description is required. Please Enter")]
        [MaxLength(200, ErrorMessage = "Description can not be longer than {1} characters.")]
        public string Description { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ExpireOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
