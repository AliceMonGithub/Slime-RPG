using UnityEngine;

namespace Sources.Factories
{
    internal enum BulletType
    {
        Default
    }

    internal class BulletFactory
    {
        private readonly Transform _prefab;

        public BulletFactory(Transform prefab)
        {
            _prefab = prefab;
        }

        public Transform Create(Vector3 position)
        {
            return Object.Instantiate(_prefab, position, Quaternion.identity);
        }
    }
}
