namespace DekoVenture
{
    public interface IStatusBarComponent
    {
        void Render(Player player);
    }

    public class HealthComponent : IStatusBarComponent
    {
        public void Render(Player player)
        {
            Clr.DGr("[");
            Clr.R($"HP:{player.Health}/{player.Vitals?.MaxHealth ?? 0}");
            Clr.DGr("]");
        }
    }

    public class StatusEffectComponent : IStatusBarComponent
    {
        public void Render(Player player)
        {
            Clr.DGr("[");
            if (player.Vitals !=null && player.Vitals.ActiveEffects.Any())
            {
                Clr.R("Status");
                for (int i = 0; i < player.Vitals.ActiveEffects.Count; i++)
                {
                    var effect = player.Vitals.ActiveEffects[i];
                    Clr.O($"{effect.Type}({effect.Duration})");

                    if (i < player.Vitals.ActiveEffects.Count -1)
                    {
                        Clr.DGr(",");
                    }
                }
            }
            Clr.DGr("]");
        }
    }

    public class StatusBar
    {
        private readonly List<IStatusBarComponent> _components = new List<IStatusBarComponent>();

        public void AddComponent(IStatusBarComponent component)
        {
            _components.Add(component);
        }


        public void Render(Player player)
        {
            Console.WriteLine();
            foreach (var component in _components)
            {
                component.Render(player);
            }
            Console.WriteLine();
            Clr.DGr(new string('-', 30));
            Console.WriteLine();
        }
    }

    public class CurrencyComponent : IStatusBarComponent
    {
        public void Render(Player player)
        {
            Clr.DY("[");
            Clr.Y($"Gold:{player.Gold}");
            Clr.DY("]");
        }
}
}