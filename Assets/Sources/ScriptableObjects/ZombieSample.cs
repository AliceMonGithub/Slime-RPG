using Sources.Properties;
using Sources.Providers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Sources.Properties
{
    internal interface IZombie
    {
        public ZombieProvider Prefab { get; }
        public TMP_Text DamagePopup { get; }

        public int MaxHealth { get; }
        public int KillAward { get; }

        public int Damage { get; }
        public float FireRate { get; }
        public float SpawnRate { get; }

        public float MaxSpeed { get; }
        public float Smooth { get; }

        public float StopDistance { get; }

        public int SpawningCount { get; }
        public int AliveCount { get; }

        public void IncreaseSpawningCount(int value);
        public void IncreaseAliveCount(int value);
        public void DecreaseAliveCount(int value);
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class ZombieSample : ScriptableObject, IZombie
    {
        [SerializeField] private ZombieProvider _prefab;
        [SerializeField] private TMP_Text _damagePopup;

        [Space]

        [SerializeField] private int _maxHealth;
        [SerializeField] private int _killAward;

        [Space]

        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private float _spawnRate;

        [Space]

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _smooth;

        [SerializeField] private float _stopDistance;

        [NonSerialized] private int _aliveCount = 0;
        [NonSerialized] private int _spawningCount = 2;

        public ZombieProvider Prefab => _prefab;
        public TMP_Text DamagePopup => _damagePopup;

        public int MaxHealth => _maxHealth;
        public int KillAward => _killAward;

        public int Damage => _damage;
        public float FireRate => _fireRate;
        public float SpawnRate => _spawnRate;

        public float MaxSpeed => _maxSpeed;
        public float Smooth => _smooth;

        public float StopDistance => _stopDistance;

        public int SpawningCount => _spawningCount;
        public int AliveCount => _aliveCount;

        public void IncreaseSpawningCount(int value)
        {
            _spawningCount += value;
        }

        public void IncreaseAliveCount(int value)
        {
            _aliveCount += value;
        }

        public void DecreaseAliveCount(int value)
        {
            _aliveCount -= value;
        }
    }
}
