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
            NearTarget nearTarget = GetNearTarget(_slimeTransform.position, systems.GetWorld());

            if (nearTarget == null) return;

            if (Vector3.Distance(_slimeTransform.position, nearTarget.Transform.position) > _slimeSample.FireDistance) return;

            foreach (int entity in _slimesFilter)
            {
                ref SlimeShotConfig slimeShot = ref _slimesPool.Get(entity);

                slimeShot.FirerateDelta += Time.deltaTime;

                if (slimeShot.FirerateDelta >= slimeShot.FireRate)
                {
                    EcsWorld world = systems.GetWorld();

                    SpawnBullet(world, slimeShot, ref slimeShot);

                    slimeShot.FirerateDelta = 0;
                }
            }
        }

        private void SpawnBullet(EcsWorld world, SlimeShotConfig slimeShot, ref SlimeShotConfig slime)
        {
            int bulletEntity = world.NewEntity();

            EcsPool<Bullet> bulletsPool = world.GetPool<Bullet>();
            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();

            ref Bullet bullet = ref bulletsPool.Add(bulletEntity);
            ref Transformable transformable = ref transformablePool.Add(bulletEntity);

            NearTarget target = GetNearTarget(_slimeTransform.position, world);

            world.GetPool<Health>().Get(target.Entity).IncreaseNextDamage(slime.Damage);

            bullet.Target = target.Transform;
            bullet.Damage = slime.Damage;
            bullet.MaxSpeed = _slimeSample.BulletSpeed;
            bullet.Smooth = _slimeSample.BulletSmooth;
            bullet.DamageDistance = _slimeSample.DamageDistance;
            bullet.Velosity = Vector3.zero;

            transformable.Transform = _factory.Create(slimeShot.ShotPoint.position);
        }

        private NearTarget GetNearTarget(Vector3 slimePosition, EcsWorld world)
        {
            EcsFilter enemiesFilter = world.Filter<Transformable>().Inc<ZombieMoveConfig>().Inc<Health>().End();
            EcsPool<Transformable> enemyTransformPool = world.GetPool<Transformable>();
            EcsPool<Health> healthPool = world.GetPool<Health>();

            Transform near = null;
            float nearDistance = 0;

            int entityIndex = 0;

            foreach (int entity in enemiesFilter)
            {
                ref Transformable transformable = ref enemyTransformPool.Get(entity);
                ref Health health = ref healthPool.Get(entity);

                if (health.Value - health.NextDamage <= 0) continue;

                if (near == null)
                {
                    near = transformable.Transform;
                    nearDistance = Vector3.Distance(slimePosition, near.position);

                    entityIndex = entity;
                }
                else if(Vector3.Distance(slimePosition, transformable.Transform.position) < nearDistance)
                {
                    near = transformable.Transform;
                    nearDistance = Vector3.Distance(slimePosition, near.position);

                    entityIndex = entity;
                }
            }

            if (near == null) return null;

            return new NearTarget(near, entityIndex);
        }
    }

    internal class NearTarget
    {
        private readonly Transform _transform;
        private readonly int _entity;

        public NearTarget(Transform transform, int entity)
        {
            _transform = transform;
            _entity = entity;
        }

        public Transform Transform => _transform;
        public int Entity => _entity;
    }
}