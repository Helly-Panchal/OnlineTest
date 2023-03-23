namespace OnlineTest.Model.Interfaces
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAnswer();
        IEnumerable<Answer> GetAnswerByQuestionId(int questionId);
        Answer GetAnswerById(int id);
        Answer AnswerExists(int testId, int questionId, string ans);
        int AddAnswer(Answer answer);
        bool UpdateAnswer(Answer answer);
        bool DeleteAnswer(Answer answer);
        
    }
}
