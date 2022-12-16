using Sources.Properties;
using Sources.Providers;
using UnityEngine;

namespace Sources.Properties
{
    internal interface IZombie
    {
        public ZombieProvider Prefab { get; }

        public int MaxHealth { get; }
        public int KillAward { get; }

        public int Damage { get; }
        public float FireRate { get; }
        public float SpawnRate { get; }

        public float MaxSpeed { get; }
        public float Smooth { get; }

        public float StopDistance { get; }
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class ZombieSample : ScriptableObject, IZombie
    {
        [SerializeField] private ZombieProvider _prefab;

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

        public ZombieProvider Prefab => _prefab;

        public int MaxHealth => _maxHealth;
        public int KillAward => _killAward;

        public int Damage => _damage;
        public float FireRate => _fireRate;
        public float SpawnRate => _spawnRate;

        public float MaxSpeed => _maxSpeed;
        public float Smooth => _smooth;

        public float StopDistance => _stopDistance;
    }
}
