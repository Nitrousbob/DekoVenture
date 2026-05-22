namespace RPG_Asn4
{

    //things with secrets can have this
    public interface ISecretKeeper
    {
        bool TryRevealSecret(Player player, Item? offeredItem = null);
    }

}