using Leopotam.EcsLite;
using UnityEngine;

namespace Sources.Ecs
{
    internal class BulletMovement : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            EcsFilter bulletsFilter = world.Filter<Transformable>().Inc<Bullet>().End();

            EcsPool<Bullet> bulletsPool = world.GetPool<Bullet>();
            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();

            foreach (int entity in bulletsFilter)
            {
                ref Bullet bullet = ref bulletsPool.Get(entity);
                ref Transformable transformable = ref transformablePool.Get(entity);

                if (Vector3.Distance(transformable.Transform.position, bullet.Target.position) < bullet.DamageDistance)
                {
                    TryTakeDamage(bullet.Damage, bullet.Target, world);

                    Object.Destroy(transformable.Transform.gameObject);

                    world.DelEntity(entity);

                    continue;
                }

                transformable.Transform.position =
                    Vector3.SmoothDamp(transformable.Transform.position, bullet.Target.position + Vector3.up, ref bullet.Velosity, bullet.Smooth, bullet.MaxSpeed);
            }
        }

        private void TryTakeDamage(int value, Transform target, EcsWorld world)
        {
            EcsFilter healthFilter = world.Filter<Transformable>().Inc<Health>().End();

            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();
            EcsPool<Health> healthPool = world.GetPool<Health>();

            foreach (int entity in healthFilter)
            {
                ref Transformable transformable = ref transformablePool.Get(entity);

                if(transformable.Transform == target)
                {
                    ref Health health = ref healthPool.Get(entity);

                    health.Value -= value;

                    Debug.Log(health.Value);
                }
            }
        }
    }
}
