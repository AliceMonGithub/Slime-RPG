using Sources.Providers;
using UnityEngine;

namespace Sources.Factories
{
    internal enum ZombieType
    {
        Default
    }

    internal class ZombieFactory
    {
        private readonly ZombieProvider _prefab;

        public ZombieFactory(ZombieProvider prefab)
        {
            _prefab = prefab;
        }

        public ZombieProvider Create(Vector3 position)
        {
            return Object.Instantiate(_prefab, position, Quaternion.identity);
        }
    }
}
