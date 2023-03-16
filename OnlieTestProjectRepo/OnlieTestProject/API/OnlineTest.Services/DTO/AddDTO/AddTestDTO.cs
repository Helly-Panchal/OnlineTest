namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddTestDTO
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ExpireOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int TechnologyId { get; set; } 
    }
}
