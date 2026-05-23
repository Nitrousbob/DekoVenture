namespace DekoVenture
{
    public interface IQuestionable
    {
        string Name { get; }
        string GetQuestionResponse();
    }
}
