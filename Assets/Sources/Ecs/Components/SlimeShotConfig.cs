using System;
using UnityEngine;

namespace Sources.Ecs
{
    internal struct SlimeShotConfig
    {
        public event Action<int, int> OnDamageValueChanged;
        public event Action<float, float> OnFireRateValueChanged;

        public Transform ShotPoint;

        private int _damage;
        private float _fireRate;

        public float FirerateDelta;

        public int Damage => _damage;
        public float FireRate => _fireRate;

        public void IncreaseDamage(int value)
        {
            OnDamageValueChanged?.Invoke(_damage, _damage + value);

            _damage += value;
        }

        public void IncreaseFireRate(float value)
        {
            OnFireRateValueChanged?.Invoke(_fireRate, _fireRate - value);

            _fireRate -= value;
        }

        public void DecreaseFireRate(float value)
        {
            OnFireRateValueChanged?.Invoke(_fireRate, _fireRate + value);

            _fireRate += value;
        }
    }
}