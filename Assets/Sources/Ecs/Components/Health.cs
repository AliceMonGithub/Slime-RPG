using System;
using UnityEngine;

namespace Sources.Ecs
{
    internal struct Health
    {
        public event Action<Health> OnHealthValueChanged;

        public GameObject GameObject;
        public int Entity;

        public float CurrentRegeneration;

        public int MaxHealth;

        private int _value;
        private int _nextDamage;

        public int Value => _value;
        public int NextDamage => _nextDamage;

        public void IncreaseHealth(int value)
        {
            if (_value + value > MaxHealth) return;

            _value += value;

            OnHealthValueChanged?.Invoke(this);
        }

        public bool DecreaseHealth(int value)
        {
            _value -= value;
            _nextDamage -= value;

            OnHealthValueChanged?.Invoke(this);

            if (_value <= 0) return true;

            return false;
        }

        public void IncreaseNextDamage(int value)
        {
            _nextDamage += value;
        }
    }
}
