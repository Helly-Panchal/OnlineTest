namespace OnlineTest.Services.DTO.AddDTO
{
    public class AddQuestionDTO
    {
        public string QuestionName { get; set; }
        public string Que { get; set; }
        public int Type { get; set; }
        public int Weightage { get; set; }
        public int SortOrder { get; set; }
        public int CreatedBy { set; get; }
        public int TestId { get; set; }
    }
}
