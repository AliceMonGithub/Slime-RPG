using System;
using UnityEngine;

namespace Sources.Ecs
{
    internal struct Health
    {
        public event Action<Health> OnHealthValueChanged;

        public GameObject GameObject;
        public int Entity;

        private int _value;
        private int _nextDamage;

        public int Value => _value;
        public int NextDamage => _nextDamage;

        public void IncreaseHealth(int value)
        {
            _value += value;

            OnHealthValueChanged?.Invoke(this);
        }

        public void DecreaseHealth(int value)
        {
            _value -= value;
            _nextDamage -= value;

            OnHealthValueChanged?.Invoke(this);
        }

        public void IncreaseNextDamage(int value)
        {
            _nextDamage += value;
        }
    }
}
