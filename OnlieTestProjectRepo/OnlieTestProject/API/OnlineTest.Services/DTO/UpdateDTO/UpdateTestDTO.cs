
namespace OnlineTest.Services.DTO.UpdateDTO
{
    public class UpdateTestDTO
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public DateTime? ExpireOn { get; set; }
        public int TechnologyId { get; set; }
    }
}