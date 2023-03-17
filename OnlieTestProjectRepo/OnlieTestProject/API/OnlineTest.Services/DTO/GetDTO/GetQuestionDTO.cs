namespace OnlineTest.Services.DTO.GetDTO
{
    public class GetQuestionDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionName { get; set; }
        public string Que { get; set; }
        public int Type { get; set; }
        public int Weightage { get; set; }
        public int SortOrder { get; set; }
    }
}
