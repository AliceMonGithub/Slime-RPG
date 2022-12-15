using UnityEngine;

namespace Sources.Factories
{
    internal enum ZombieType
    {
        Default
    }

    internal class ZombieFactory
    {
        private readonly Transform _prefab;

        public ZombieFactory(Transform prefab)
        {
            _prefab = prefab;
        }

        public Transform Create(Vector3 position)
        {
            return Object.Instantiate(_prefab, position, Quaternion.identity);
        }
    }
}
