using Sources.Properties;
using UnityEngine;

namespace Sources.Properties
{
    internal interface ISlime
    {
        public int Damage { get; }
        public float Firerate { get; }

        public Transform BulletPrefab { get; }
        public float BulletSpeed { get; }
        public float BulletSmooth { get; }
        public float DamageDistance { get; }
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class SlimeSample : ScriptableObject, ISlime
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _firerate;

        [Space]

        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletSmooth;
        [SerializeField] private float _damageDistance;

        public int Damage => _damage;
        public float Firerate => _firerate;

        public Transform BulletPrefab => _bulletPrefab;
        public float BulletSpeed => _bulletSpeed;
        public float BulletSmooth => _bulletSmooth;
        public float DamageDistance => _damageDistance;
    }
}
