using System;

namespace Sources.Ecs
{
    internal struct Health
    {
        public event Action<int> OnHealthValueChanged;

        public int Value;

        public void TakeDamage(int damage)
        {
            Value -= damage;

            OnHealthValueChanged?.Invoke(Value);
        }
    }
}
