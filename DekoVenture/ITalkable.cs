namespace DekoVenture
{
    public interface ITalkable
    {
        string Name { get; }
        string GetTalkResponse();
    }
}
