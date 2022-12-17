using Leopotam.EcsLite;
using Sources.Factories;
using Sources.Properties;
using System.Collections;
using UnityEngine;

namespace Sources.Ecs
{
    internal class ZombiesSpawn : IEcsInitSystem, IEcsRunSystem
    {
        private readonly int DeadHash = Animator.StringToHash("Dead");

        private readonly Transform[] _spawnPoints;
        private readonly IZombie _zombieSample;
        private readonly IPlayerStats _playerStats;
        private readonly Transform _target;

        private readonly ZombieFactory _factory;

        private readonly ICoroutineRunner _coroutineRunner;

        private EcsWorld _ecsWorld;

        private float _delta;

        public ZombiesSpawn(ZombieFactory factory, Transform target, Transform[] spawnPoints, IPlayerStats playerStats, IZombie zombieSample, ICoroutineRunner coroutineRunner)
        {
            _spawnPoints = spawnPoints;
            _zombieSample = zombieSample;
            _playerStats = playerStats;
            _target = target;
            _coroutineRunner = coroutineRunner;

            _factory = factory;
        }

        private Transform RandomSpawnPoint => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            if(_zombieSample.AliveCount == 0)
            {
                Debug.Log("Spawn wave");
                Debug.Log("Count " + _zombieSample.SpawningCount);

                int count = _zombieSample.SpawningCount;



                for (int i = 0; i < count; i++)
                {
                    _coroutineRunner.InvokeCoroutine(SpawnLoop());

                     //_coroutineRunner.Invoke(nameof(SpawnZombie), 0.8f);

                    _zombieSample.IncreaseAliveCount(1);
                }
            }

            //_delta += Time.deltaTime;

            //if (_delta >= _zombieSample.SpawnRate)
            //{
            //    SpawnZombie(systems.GetWorld());

            //    _delta = 0f;
            //}
        }

        public IEnumerator SpawnLoop()
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));

            SpawnZombie();
        }

        public void SpawnZombie()
        {
            int zombieEntity = _ecsWorld.NewEntity();

            EcsPool<ZombieMoveConfig> zombiesPool = _ecsWorld.GetPool<ZombieMoveConfig>();
            EcsPool<Transformable> transformablePool = _ecsWorld.GetPool<Transformable>();
            EcsPool<Health> healthPool = _ecsWorld.GetPool<Health>();

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

            health.IncreaseMaxHealth(_zombieSample.MaxHealth);
            health.IncreaseHealth(_zombieSample.MaxHealth);
            health.Entity = zombieEntity;
            health.GameObject = transformable.Transform.gameObject;

            health.OnHealthValueChanged += TryDie;

            if(Random.Range(0, 4) == 0)
            {
                _zombieSample.IncreaseSpawningCount(1);
            }
        }

        private void TryDie(Health health)
        {
            ref ZombieMoveConfig zombie = ref _ecsWorld.GetPool<ZombieMoveConfig>().Get(health.Entity);

            if(health.Value <= 0)
            {
                Object.Destroy(health.GameObject, 3);

                zombie.Provider.Animator.SetTrigger(DeadHash);
                zombie.Provider.BarCanvas.SetActive(false);

                _ecsWorld.DelEntity(health.Entity);

                _playerStats.IncreaseCoins(_zombieSample.KillAward);

                _zombieSample.DecreaseAliveCount(1);
            }
        }
    }
}
