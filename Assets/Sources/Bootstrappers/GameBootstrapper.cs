using Leopotam.EcsLite;
using Sources.Ecs;
using Sources.Factories;
using Sources.ScriptableObjects;
using UnityEngine;

namespace Sources.Bootstrappers
{
    internal class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Transform[] _zombieSpawnPoints;

        [Space]

        [SerializeField] private ZombieSample _zombieProperties;
        [SerializeField] private SlimeSample _slimeProperties;

        [Space]

        [SerializeField] private Transform _slimeTransform;
        [SerializeField] private Transform _shotPoint;

        private EcsWorld _world;

        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);

            _systems
                .Add(new SlimeInit(_slimeProperties, _slimeTransform, _shotPoint))
                .Add(new ZombiesSpawn(new ZombieFactory(_zombieProperties.Prefab), _slimeTransform, _zombieSpawnPoints, _zombieProperties))
                .Add(new ZombieMovement(_world, _slimeTransform))
                .Add(new SlimeShoting(_world, _slimeProperties, _slimeTransform, new BulletFactory(_slimeProperties.BulletPrefab)))
                .Add(new BulletMovement())
                //.Add(new SlimeShoting(_world, _slimeProperties, _slimeTransform, new BulletFactory(_slimeProperties.BulletPrefab))
                .Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _systems = null;

            _world.Destroy();
            _world = null;
        }
    }
}
