using Leopotam.EcsLite;
using Sources.Properties;
using UnityEngine;

namespace Sources.Ecs
{
    internal class SlimeInit : IEcsInitSystem
    {
        private readonly ISlime _slimeSample;

        private readonly Transform _slimeTransform;
        private readonly Transform _shotPoint;

        public SlimeInit(ISlime slimeSample, Transform slimeTransform, Transform shotPoint)
        {
            _slimeSample = slimeSample;

            _slimeTransform = slimeTransform;
            _shotPoint = shotPoint;
        }

        public void Init(IEcsSystems systems)
        {
            InitSlime(systems.GetWorld());
        }

        public void InitSlime(EcsWorld world)
        {
            int slimeEntity = world.NewEntity();

            EcsPool<SlimeShotConfig> slimesPool = world.GetPool<SlimeShotConfig>();
            EcsPool<Transformable> transformablePool = world.GetPool<Transformable>();
            EcsPool<Health> healthPool = world.GetPool<Health>();

            ref SlimeShotConfig slimeShot = ref slimesPool.Add(slimeEntity);
            ref Transformable transformable = ref transformablePool.Add(slimeEntity);
            ref Health health = ref healthPool.Add(slimeEntity);

            slimeShot.ShotPoint = _shotPoint;
            slimeShot.IncreaseDamage(_slimeSample.StartDamage);
            slimeShot.DecreaseFireRate(_slimeSample.StartFireRate);

            transformable.Transform = _slimeTransform;

            health.IncreaseMaxHealth(_slimeSample.MaxHealth);
            health.IncreaseHealth(health.MaxHealth);
        }
    }
}
