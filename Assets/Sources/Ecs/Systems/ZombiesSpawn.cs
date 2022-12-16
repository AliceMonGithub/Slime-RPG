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
        private readonly IPlayerStats _playerStats;
        private readonly Transform _target;

        private readonly ZombieFactory _factory;

        private EcsWorld _ecsWorld;

        private float _delta;

        public ZombiesSpawn(ZombieFactory factory, Transform target, Transform[] spawnPoints, IPlayerStats playerStats, IZombie zombieSample)
        {
            _spawnPoints = spawnPoints;
            _zombieSample = zombieSample;
            _playerStats = playerStats;
            _target = target;

            _factory = factory;
        }

        private Transform RandomSpawnPoint => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();

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

            zombie.AttackFireRate = _zombieSample.FireRate;
            zombie.StopDistance = _zombieSample.StopDistance;
            zombie.Velosity = Vector3.zero;

            zombie.Provider = _factory.Create(RandomSpawnPoint.position);
            transformable.Transform = zombie.Provider.Root;

            transformable.Transform.LookAt(_target.position);
            
            Vector3 rotation = transformable.Transform.eulerAngles;

            rotation.y = 90;

            transformable.Transform.eulerAngles = rotation;

            health.MaxHealth = _zombieSample.MaxHealth;
            health.IncreaseHealth(_zombieSample.MaxHealth);
            health.Entity = zombieEntity;
            health.GameObject = transformable.Transform.gameObject;

            health.OnHealthValueChanged += TryDie;
        }

        private void TryDie(Health health)
        {
            if(health.Value <= 0)
            {
                Object.Destroy(health.GameObject);

                _ecsWorld.DelEntity(health.Entity);

                _playerStats.IncreaseCoins(_zombieSample.KillAward);

                EcsFilter slimeFilter = _ecsWorld.Filter<SlimeShotConfig>().Inc<Health>().End();

                EcsPool<Health> healthPool = _ecsWorld.GetPool<Health>();

                foreach (int entity in slimeFilter)
                {
                    ref Health slimeHealth = ref healthPool.Get(entity);

                    slimeHealth.IncreaseHealth(1);
                }
            }
        }
    }
}
