using Sources.Properties;
using System;
using UnityEngine;

namespace Sources.Properties
{
    internal interface ISlime
    {
        public Action<int, int> OnDamageValueChanged { get; set; }
        public Action<float, float> OnFireRateValueChanged { get; set; }

        public int MaxHealth { get; }
        public int StartDamage { get; }
        public float StartFireRate { get; }

        public float RegenerationValue { get; }
        public float RegenerationRate { get; }

        public Transform BulletPrefab { get; }
        public float BulletSpeed { get; }
        public float BulletSmooth { get; }
        public float DamageDistance { get; }
        public float FireDistance { get; }
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class SlimeSample : ScriptableObject, ISlime
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _startDamage;
        [SerializeField] private float _startFireRate;

        [Space]

        [SerializeField] private float _regenerationValue;
        [SerializeField] private float _regenerationRate;

        [Space]

        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletSmooth;
        [SerializeField] private float _damageDistance;
        [SerializeField] private float _fireDistance;

        public Action<int, int> OnDamageValueChanged { get; set; }
        public Action<float, float> OnFireRateValueChanged { get; set; }

        public int MaxHealth => _maxHealth;
        public int StartDamage => _startDamage;
        public float StartFireRate => _startFireRate;

        public float RegenerationValue => _regenerationValue;
        public float RegenerationRate => _regenerationRate;

        public Transform BulletPrefab => _bulletPrefab;
        public float BulletSpeed => _bulletSpeed;
        public float BulletSmooth => _bulletSmooth;
        public float DamageDistance => _damageDistance;
        public float FireDistance => _fireDistance;
    }
}
