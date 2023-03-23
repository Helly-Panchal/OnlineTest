namespace OnlineTest.Model.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuestionByTestId(int testId);
        Question GetQuestionById(int id);
        Question QuestionExists(Question question);
        int AddQuestion(Question question);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(Question question);
       
    }
}
