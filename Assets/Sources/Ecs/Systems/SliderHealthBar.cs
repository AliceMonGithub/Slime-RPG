using Leopotam.EcsLite;
using Sources.Factories;
using Sources.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Ecs
{
    internal class SliderHealthBar : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Image _slimeSlider;
        private readonly ISlime _slimeSample;

        private readonly PopupFactory _popupFactory;

        private EcsFilter _slimesFilter;
        EcsPool<Health> _healthPool;

        private EcsWorld _ecsWorld;

        public SliderHealthBar(Image slimeSlider, ISlime slimeSample, PopupFactory popupFactory)
        {
            _slimeSlider = slimeSlider;
            _slimeSample = slimeSample;

            _popupFactory = popupFactory;
        }

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();

            _slimesFilter = _ecsWorld.Filter<SlimeShotConfig>().Inc<Health>().End();
            _healthPool = _ecsWorld.GetPool<Health>();

            InitSlimeBar();

            BulletMovement.OnHit += UpdateZombieBar;
            BulletMovement.OnHit += CreateDamagePopup;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _slimesFilter)
            {
                ref Health health = ref _healthPool.Get(entity);

                health.CurrentRegeneration += Time.deltaTime;

                if(health.CurrentRegeneration >= _slimeSample.RegenerationRate)
                {
                    health.IncreaseHealth(_slimeSample.RegenerationValue);

                    health.CurrentRegeneration = 0;
                }
            }
        }

        private void InitSlimeBar()
        {
            foreach (int entity in _slimesFilter)
            {
                ref Health health = ref _healthPool.Get(entity);

                health.OnHealthValueChanged += UpdateSlimeBar;
                health.OnHealthValueChanged += TryRestart;
            }
        }

        private void UpdateZombieBar(int entity, int damage)
        {
            ref ZombieMoveConfig zombie = ref _ecsWorld.GetPool<ZombieMoveConfig>().Get(entity);
            ref Health health = ref _ecsWorld.GetPool<Health>().Get(entity);

            zombie.Provider.BarSlider.fillAmount = (float)health.Value / health.MaxHealth;
        }

        private void CreateDamagePopup(int entity, int damage)
        {
            ref ZombieMoveConfig zombie = ref _ecsWorld.GetPool<ZombieMoveConfig>().Get(entity);

            _popupFactory.Create(zombie.Provider.DamagePopupPoint.position, zombie.Provider.DamagePopupPoint).text = "-" + damage;
        }

        private void UpdateSlimeBar(Health health)
        {
            _slimeSlider.fillAmount = (float)health.Value / health.MaxHealth;
        }

        private void TryRestart(Health health)
        {
            if(health.Value <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
