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

            ref SlimeShotConfig slimeShot = ref slimesPool.Add(slimeEntity);
            ref Transformable transformable = ref transformablePool.Add(slimeEntity);

            slimeShot.ShotPoint = _shotPoint;
            slimeShot.Damage = _slimeSample.Damage;
            slimeShot.Firerate = _slimeSample.Firerate;

            transformable.Transform = _slimeTransform;
        }
    }
}
