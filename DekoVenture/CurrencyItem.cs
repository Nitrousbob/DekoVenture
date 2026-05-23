namespace DekoVenture
{
    public class CurrencyItem : Item
    {
        public int Amount {get; set;}
        public CurrencyItem(){}
        public CurrencyItem(string name, string description, int amount) : base(name, description)
        {
            Amount = amount;
        }
    }
}