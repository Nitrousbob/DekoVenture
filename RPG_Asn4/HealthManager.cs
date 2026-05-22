namespace RPG_Asn4
{
    public enum EffectType
    {
        Poison,
        Bleeding,
        Blinded,
        Stunned,
        SkinsuitPrep,
    }

    public class StatusEffect
    {
        public EffectType Type { get; set;}
        public int Duration { get; set; }
        public int Magnitude {get; set; }
        public StatusEffect(){}
        public StatusEffect(EffectType type, int duration, int magnitude)
        {
            Type = type;
            Duration = duration;
            Magnitude = magnitude;
        
        }
    }

    public class HealthManager
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public List<StatusEffect> ActiveEffects {get; set;} = new List<StatusEffect>();
        public HealthManager(){}
        public HealthManager(int maxHealth)
        {
            MaxHealth = Math.Max(1, maxHealth);
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth < 0) CurrentHealth = 0;
        }
        public int Heal(int amount)
        {
            int before = CurrentHealth;
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
            return CurrentHealth - before;
        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        public void AddEffect(StatusEffect effect)
        {
            ActiveEffects.Add(effect);
        }

        public void CureEffect(EffectType type)
        {
            ActiveEffects.RemoveAll(e => e.Type == type);
        }
        
        public void TickEffects()
        {
            for (int i = ActiveEffects.Count - 1; i >= 0; i--)
            {
                var effect = ActiveEffects[i];
                
                if (effect.Type == EffectType.Poison || effect.Type == EffectType.Bleeding)
                {
                    TakeDamage(effect.Magnitude);
                }

                effect.Duration--;
                if (effect.Duration <= 0)
                {
                    ActiveEffects.RemoveAt(i);
                }
            }
        }
    }
}