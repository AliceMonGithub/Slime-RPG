using Leopotam.EcsLite;
using UnityEngine;

namespace Sources.Ecs
{
    internal class ZombieMovement : IEcsRunSystem
    {
        private readonly EcsFilter _zombiesFilter;
        private readonly EcsPool<ZombieMoveConfig> _zombiesPool;
        private readonly EcsPool<Transformable> _transformablePool;

        private readonly Transform _target;

        public ZombieMovement(EcsWorld world, Transform target)
        {
            _zombiesFilter = world.Filter<Transformable>().Inc<ZombieMoveConfig>().End();

            _zombiesPool = world.GetPool<ZombieMoveConfig>();
            _transformablePool = world.GetPool<Transformable>();

            _target = target;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _zombiesFilter)
            {
                ref Transformable transformable = ref _transformablePool.Get(entity);
                ref ZombieMoveConfig zombie = ref _zombiesPool.Get(entity);

                if (Vector3.Distance(transformable.Transform.position, _target.position) <= zombie.StopDistance) continue;

                transformable.Transform.position =
                    Vector3.SmoothDamp(transformable.Transform.position, _target.position, ref zombie.Velosity, zombie.Smooth, zombie.MaxSpeed);
            }
        }
    }
}
