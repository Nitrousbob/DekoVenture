namespace DekoVenture
{
    public static class ItemFactory
    {
        public static CurrencyItem CreateGoldCoin(int amount = 5)
        {
            return new CurrencyItem("Shiny coin", "A slightly tarnished but shiny gold coin.", amount);
        }

        public static Consumable CreateHealthDrink(int quantity = 1)
        {
            return new Consumable("Health Drink", "A small vial of red liquid (Heals 10hp)", 10, EffectType.Poison)
            {
                Quantity = quantity
            };
        }
        public static Consumable CreateAntidote(int quantity = 1)
        {
            return new Consumable("Antidote", "A bitter green liquid used to cure poison.", 0, EffectType.Poison)
            {
                Quantity = quantity
            };
        }

        public static Consumable CreateLotion(int quantity = 1)
        {
            return new Consumable("Lotion", "Buffalo Bills Super Hydrating Lotion, The lotion goes on the skin", 0, EffectType.SkinsuitPrep)
            {
                Quantity = quantity
            };
        }

        public static Consumable CreatePoison(int quantity = 1)
        {
            return new Consumable("Poison", "A dark sweet smelling liquid", 0, EffectType.Poison)
            {
            Quantity = quantity,
            Apply = new List<StatusEffect>
                {
                    new StatusEffect(EffectType.Poison, 3, 2)
                }
            };
        }

        public static Consumable CreateBleeding(int quantity = 1)
        {
            return new Consumable("Glass shards", "A pile of broken glass", 0, EffectType.Bleeding)
            {
            Quantity = quantity,
            Apply = new List<StatusEffect>
                {
                    new StatusEffect(EffectType.Bleeding, 3, 2)
                }
            };
        }
        public static Consumable CreateBandage(int quantity = 1)
        {
            return new Consumable("Bandage", "Stops Bleeding", 0, EffectType.Bleeding)
            {
            Quantity = quantity
            };
        }
    }
}