using Leopotam.EcsLite;
using Sources.Properties;
using UnityEngine;

namespace Sources.Ecs
{
    internal class ZombieMovement : IEcsRunSystem
    {
        private readonly int MovingHash = Animator.StringToHash("Moving");

        private readonly EcsFilter _zombiesFilter;
        private readonly EcsFilter _slimeFilter;

        private readonly EcsPool<ZombieMoveConfig> _zombiesPool;
        private readonly EcsPool<Transformable> _transformablePool;
        private readonly EcsPool<Health> _healthPool;

        private readonly IZombie _zombieSample;
        private readonly Transform _target;

        public ZombieMovement(EcsWorld world, Transform target, IZombie zombieSample)
        {
            _zombiesFilter = world.Filter<Transformable>().Inc<ZombieMoveConfig>().End();
            _slimeFilter = world.Filter<SlimeShotConfig>().Inc<Health>().End();

            _zombiesPool = world.GetPool<ZombieMoveConfig>();
            _transformablePool = world.GetPool<Transformable>();
            _healthPool = world.GetPool<Health>();

            _zombieSample = zombieSample;
            _target = target;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _zombiesFilter)
            {
                ref Transformable transformable = ref _transformablePool.Get(entity);
                ref ZombieMoveConfig zombie = ref _zombiesPool.Get(entity);

                if (Vector3.Distance(transformable.Transform.position, _target.position) <= zombie.StopDistance)
                {
                    zombie.Provider.Animator.SetBool(MovingHash, false);

                    zombie.CurrentFireRate += Time.deltaTime;

                    if(zombie.CurrentFireRate >= zombie.AttackFireRate)
                    {
                        foreach (int slimeEnitity in _slimeFilter)
                        {
                            ref Health health = ref _healthPool.Get(slimeEnitity);

                            health.DecreaseHealth(_zombieSample.Damage);
                        }

                        zombie.CurrentFireRate = 0;
                    }

                    continue;
                }

                zombie.Provider.Animator.SetBool(MovingHash, true);

                transformable.Transform.position =
                    Vector3.SmoothDamp(transformable.Transform.position, _target.position, ref zombie.Velosity, zombie.Smooth, zombie.MaxSpeed);
            }
        }
    }
}
