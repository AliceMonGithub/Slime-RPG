using System;
using UnityEngine;

namespace Sources.Ecs
{
    internal struct Health
    {
        public event Action<Health> OnHealthValueChanged;
        public event Action<int, int> OnMaxHealthValueChanged;

        public GameObject GameObject;
        public int Entity;

        public float CurrentRegeneration;

        public int _maxHealth;

        private int _value;
        private int _nextDamage;

        public int MaxHealth => _maxHealth;

        public int Value => _value;
        public int NextDamage => _nextDamage;

        public void IncreaseHealth(int value)
        {
            if (_value + value > _maxHealth) return;

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

        public void IncreaseMaxHealth(int value)
        {
            OnMaxHealthValueChanged?.Invoke(_maxHealth, _maxHealth + value);

            _maxHealth += value;

            OnHealthValueChanged?.Invoke(this);
        }

        public void IncreaseNextDamage(int value)
        {
            _nextDamage += value;
        }
    }
}
