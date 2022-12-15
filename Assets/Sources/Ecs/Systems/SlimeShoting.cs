using Leopotam.EcsLite;
using Sources.Factories;
using Sources.Properties;
using UnityEngine;

namespace Sources.Ecs
{
    internal class SlimeShoting : IEcsRunSystem
    {
        private readonly EcsFilter _slimesFilter;

        private readonly EcsPool<SlimeShotConfig> _slimesPool;

        private readonly ISlime _slimeSample;
        private readonly Transform _slimeTransform;

        private readonly BulletFactory _factory;

        public SlimeShoting(EcsWorld world, ISlime slimeSample, Transform slimeTransform, BulletFactory factory)
        {
            _slimesFilter = world.Filter<Transformable>().Inc<SlimeShotConfig>().End();

            _slimesPool = world.GetPool<SlimeShotConfig>();

            _slimeSample = slimeSample;
            _slimeTransform = slimeTransform;

            _factory = factory;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _slimesFilter)
            {
                ref SlimeShotConfig slimeShot = ref _slimesPool.Get(entity);

                slimeShot.FirerateDelta += Time.deltaTime;

                if (slimeShot.FirerateDelta >= slimeShot.Firerate)
                {
                    EcsWorld world = systems.GetWorld();

                    SpawnBullet(world, slimeShot);

                    slimeShot.FirerateDelta = 0;
                }
            }
        }

        private void SpawnBullet(EcsWorld world, SlimeShotConfig slimeShot)
        {
            int bulletEntity = world.NewEntity();

            EcsPool<Bullet> bulletsPool = world.GetPool<Bullet>();
            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();

            ref Bullet bullet = ref bulletsPool.Add(bulletEntity);
            ref Transformable transformable = ref transformablePool.Add(bulletEntity);

            bullet.Target = GetNearTarget(_slimeTransform.position, world);
            bullet.Damage = _slimeSample.Damage;
            bullet.MaxSpeed = _slimeSample.BulletSpeed;
            bullet.Smooth = _slimeSample.BulletSmooth;
            bullet.DamageDistance = _slimeSample.DamageDistance;
            bullet.Velosity = Vector3.zero;

            transformable.Transform = _factory.Create(slimeShot.ShotPoint.position);
        }

        private Transform GetNearTarget(Vector3 slimePosition, EcsWorld world)
        {
            EcsFilter enemiesFilter = world.Filter<Transformable>().Inc<ZombieMoveConfig>().End();
            EcsPool<Transformable> enemyTransformPool = world.GetPool<Transformable>();

            Transform near = null;
            float nearDistance = 0;

            foreach (int entity in enemiesFilter)
            {
                ref Transformable transformable = ref enemyTransformPool.Get(entity);

                if (near == null)
                {
                    near = transformable.Transform;
                    nearDistance = Vector3.Distance(slimePosition, near.position);
                }
                else if(Vector3.Distance(slimePosition, transformable.Transform.position) < nearDistance)
                {
                    near = transformable.Transform;
                    nearDistance = Vector3.Distance(slimePosition, near.position);
                }
            }

            return near;
        }
    }
}