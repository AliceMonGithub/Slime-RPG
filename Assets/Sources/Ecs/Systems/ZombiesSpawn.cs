using Leopotam.EcsLite;
using Sources.Factories;
using Sources.Properties;
using UnityEngine;

namespace Sources.Ecs
{
    internal class ZombiesSpawn : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Transform[] _spawnPoints;
        private readonly IZombie _zombieSample;
        private readonly Transform _target;

        private readonly ZombieFactory _factory;

        private float _delta;

        public ZombiesSpawn(ZombieFactory factory, Transform target, Transform[] spawnPoints, IZombie zombieSample)
        {
            _spawnPoints = spawnPoints;
            _zombieSample = zombieSample;
            _target = target;

            _factory = factory;
        }

        private Transform RandomSpawnPoint => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        public void Init(IEcsSystems systems)
        {
            SpawnZombie(systems.GetWorld());
        }

        public void Run(IEcsSystems systems)
        {
            _delta += Time.deltaTime;

            if (_delta >= _zombieSample.SpawnRate)
            {
                SpawnZombie(systems.GetWorld());

                _delta = 0f;
            }
        }

        public void SpawnZombie(EcsWorld world)
        {
            int zombieEntity = world.NewEntity();

            EcsPool<ZombieMoveConfig> zombiesPool = world.GetPool<ZombieMoveConfig>();
            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();
            EcsPool<Health> healthPool = world.GetPool<Health>();

            ref ZombieMoveConfig zombie = ref zombiesPool.Add(zombieEntity);
            ref Transformable transformable = ref transformablePool.Add(zombieEntity);
            ref Health health = ref healthPool.Add(zombieEntity);

            zombie.Smooth = _zombieSample.Smooth;
            zombie.MaxSpeed = _zombieSample.MaxSpeed;

            zombie.StopDistance = _zombieSample.StopDistance;
            zombie.Velosity = Vector3.zero;

            transformable.Transform = _factory.Create(RandomSpawnPoint.position);
            transformable.Transform.LookAt(_target.position, Vector3.up);

            health.Value = _zombieSample.Health;
        }
    }
}
