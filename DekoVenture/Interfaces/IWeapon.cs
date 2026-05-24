namespace DekoVenture
{
    public interface IWeapon
    {
        string Name { get; }
        int MinDamage { get; }
        int MaxDamage { get; }
        int GetDamage();
    }
}