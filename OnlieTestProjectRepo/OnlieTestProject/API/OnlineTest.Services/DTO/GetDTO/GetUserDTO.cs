using System.ComponentModel.DataAnnotations;

namespace OnlineTest.Services.DTO.GetDTO
{
    public class GetUserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobileNo { get; set; }
        public string Email { get; set; }
    }
}