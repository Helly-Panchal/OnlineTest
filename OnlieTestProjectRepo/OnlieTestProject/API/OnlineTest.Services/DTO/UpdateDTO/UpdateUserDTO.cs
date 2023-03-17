using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.UpdateDTO
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
    }
}