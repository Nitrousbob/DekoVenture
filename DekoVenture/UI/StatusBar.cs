
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
            if (player.Vitals != null && player.Vitals.ActiveEffects.Any())
            {
                for (int i = 0; i < player.Vitals.ActiveEffects.Count; i++)
                {
                    var effect = player.Vitals.ActiveEffects[i];
                    UI.ShowStatusEffect($"{GetStatusDescription(effect)}");

                    if (i < player.Vitals.ActiveEffects.Count - 1)
                    {
                        Clr.DGr(" ");
                    }
                }
            }
            Clr.DGr("]");
        }

        private string GetStatusDescription(StatusEffect effect)
        {
            return effect.Type switch
            {
                EffectType.Poison => $"<G>Poison</G>(<G>{effect.Duration}</G>)",
                EffectType.Bleeding => $"<R>Bleed</R>(<R>{effect.Duration}</R>)",
                EffectType.Blinded => $"<W>Blind</W>(<W>{effect.Duration}</W>)",
                EffectType.Stunned => $"<O>Stun</O>(<O>{effect.Duration}</O>)",
                _ => $"{effect.Type}({effect.Duration})"
            };
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
            Clr.DGr("[");
            Clr.G($"{player.Gold}");
            Clr.DGr("]");
        }
    }
}
