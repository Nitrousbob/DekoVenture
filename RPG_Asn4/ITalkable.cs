namespace RPG_Asn4
{
    public interface ITalkable
    {
        string Name { get; }
        string GetTalkResponse();
    }
}
