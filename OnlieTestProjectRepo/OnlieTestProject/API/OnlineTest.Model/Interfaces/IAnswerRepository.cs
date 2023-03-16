namespace OnlineTest.Model.Interfaces
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetAnswer();
        Answer GetAnswerById(int id);
        bool AddAnswer(Answer answer);
        bool UpdateAnswer(Answer answer);
    }
}
