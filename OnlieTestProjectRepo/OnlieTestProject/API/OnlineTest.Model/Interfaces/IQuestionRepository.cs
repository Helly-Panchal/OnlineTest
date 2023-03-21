namespace OnlineTest.Model.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuestionByTestId(int testId);
        Question GetQuestionById(int id);
        int AddQuestion(Question question);
        bool UpdateQuestion(Question question);
        bool DeleteQuestion(Question question);
        bool IsQuestionExists(int testId, string que);
    }
}
