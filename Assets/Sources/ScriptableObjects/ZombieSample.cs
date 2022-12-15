using Sources.Properties;
using UnityEngine;

namespace Sources.Properties
{
    internal interface IZombie
    {
        public Transform Prefab { get; }

        public int Health { get; }

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
        [SerializeField] private Transform _prefab;

        [Space]

        [SerializeField] private int _health;

        [Space]

        [SerializeField] private float _spawnRate;

        [Space]

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _smooth;

        [SerializeField] private float _stopDistance;

        public Transform Prefab => _prefab;

        public int Health => _health;

        public float SpawnRate => _spawnRate;

        public float MaxSpeed => _maxSpeed;
        public float Smooth => _smooth;

        public float StopDistance => _stopDistance;
    }
}
