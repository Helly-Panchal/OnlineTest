using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddAnswerDTO
    {
        public string Title { get; set; }
        public int CreatedBy { set; get; }
        public DateTime CreateOn { set; get; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
