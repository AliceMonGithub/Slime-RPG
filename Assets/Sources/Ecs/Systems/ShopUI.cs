using Leopotam.EcsLite;
using Sources.Properties;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ecs
{
    [Serializable]
    internal class ShopUIConfig
    {
        [SerializeField] private Button _damageUpgrade;
        [SerializeField] private Button _fireRateUpgrade;

        [Space]

        [SerializeField] private float _countersSmooth;

        [Space]

        [SerializeField] private TMP_Text _damageCounter;
        [SerializeField] private TMP_Text _fireRateCounter;

        [Space]

        [SerializeField] private TMP_Text _damageLevelCounter;
        [SerializeField] private TMP_Text _fireRateLevelCounter;

        [SerializeField] private TMP_Text _damageValueCounter;
        [SerializeField] private TMP_Text _fireRateValueCounter;

        [SerializeField] private TMP_Text _damageUpgradeCostCounter;
        [SerializeField] private TMP_Text _fireRateUpgradeCostCounter;

        public Button DamageUpgrade => _damageUpgrade;
        public Button FireRateUpgrade => _fireRateUpgrade;

        public float CountersSmooth => _countersSmooth;

        public TMP_Text DamageCounter => _damageCounter;
        public TMP_Text FireRateCounter => _fireRateCounter;

        public TMP_Text DamageLevelCounter => _damageLevelCounter;
        public TMP_Text FireRateLevelCounter => _fireRateLevelCounter;

        public TMP_Text DamageValueCounter => _damageValueCounter;
        public TMP_Text FireRateValueCounter => _fireRateValueCounter;

        public TMP_Text DamageUpgradeCostCounter => _damageUpgradeCostCounter;
        public TMP_Text FireRateUpgradeCostCounter => _fireRateUpgradeCostCounter;
    }

    internal class ShopUI : IEcsInitSystem
    {
        private readonly ShopUIConfig _config;

        private readonly IPlayerStats _playerStats;
        private readonly ISlime _slimeSample;
        private readonly IShop _shop;

        private readonly ICoroutineRunner _coroutineRunner;

        private EcsWorld _ecsWorld;

        public ShopUI(ShopUIConfig config, IPlayerStats playerStats, IShop shop, ISlime slimeSample, ICoroutineRunner coroutineRunner)
        {
            _config = config;

            _playerStats = playerStats;
            _slimeSample = slimeSample;
            _shop = shop;

            _coroutineRunner = coroutineRunner;
        }

        public void Init(IEcsSystems systems)
        {
            _ecsWorld = systems.GetWorld();

            EcsFilter slimesFilter = _ecsWorld.Filter<SlimeShotConfig>().End();
            EcsPool<SlimeShotConfig> pool = _ecsWorld.GetPool<SlimeShotConfig>();

            foreach (int entity in slimesFilter)
            {
                ref SlimeShotConfig slime = ref pool.Get(entity);

                UpdateDamageCounter(0, slime.Damage);
                UpdateFireRateCounter(0, slime.FireRate);

                slime.OnDamageValueChanged += UpdateDamageCounter;
                slime.OnFireRateValueChanged += UpdateFireRateCounter;
            }

            _shop.OnDamageUpgradeCostChanged += UpdateDamageCostCounter;
            _shop.OnFireRateUpgradeCostChanged += UpdateFireRateCostCounter;

            _shop.OnDamageLevelChanged += UpdateDamageLevelCounter;
            _shop.OnFireRateLevelChanged += UpdateFireRateLevelCounter;

            _shop.IncreaseDamageUpgradeCost(_shop.StartDamageUpgradeCost);
            _shop.IncreaseFireRateUpgradeCost(_shop.StartFireRateUpgradeCost);

            _shop.IncreaseDamageUpgradeValue(_shop.StartDamageUpgradeValue);
            _shop.IncreaseFireRateUpgradeValue(_shop.StartFireRateUpgradeValue);

            _shop.IncreaseDamageLevel(_shop.StartDamageLevel);
            _shop.IncreaseFireRateLevel(_shop.StartFireRateLevel);

            _config.DamageUpgrade.onClick.AddListener(TryUpgradeDamage);
            _config.FireRateUpgrade.onClick.AddListener(TryUpgradeFireRate);
        }

        private void TryUpgradeDamage()
        {
            if(_shop.DamageUpgradeCost <= _playerStats.Coins)
            {
                _playerStats.DecreaseCoins(_shop.DamageUpgradeCost);
                _shop.IncreaseDamageLevel(1);
                _shop.IncreaseDamageUpgradeCost(_shop.DamageUpgradeIncrease);

                EcsFilter slimesFilter = _ecsWorld.Filter<SlimeShotConfig>().End();
                EcsPool<SlimeShotConfig> pool = _ecsWorld.GetPool<SlimeShotConfig>();

                foreach (int entity in slimesFilter)
                {
                    ref SlimeShotConfig slime = ref pool.Get(entity);

                    slime.IncreaseDamage(_shop.DamageUpgradeValue);
                }
            }
        }

        private void TryUpgradeFireRate()
        {
            if (_shop.FireRateUpgradeCost <= _playerStats.Coins)
            {
                EcsFilter slimesFilter = _ecsWorld.Filter<SlimeShotConfig>().End();
                EcsPool<SlimeShotConfig> pool = _ecsWorld.GetPool<SlimeShotConfig>();

                foreach (int entity in slimesFilter)
                {
                    ref SlimeShotConfig slime = ref pool.Get(entity);

                    if (slime.FireRate - _shop.FireRateUpgradeValue <= 0) return;

                    slime.IncreaseFireRate(_shop.FireRateUpgradeValue);
                }

                _playerStats.DecreaseCoins(_shop.FireRateUpgradeCost);
                _shop.IncreaseFireRateLevel(1);
                _shop.IncreaseFireRateUpgradeCost(_shop.FireRateUpgradeIncrease);
            }
        }

        private void UpdateDamageCounter(int oldValue, int newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.DamageCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.DamageCounter, newValue, oldValue));
        }

        private void UpdateFireRateCounter(float oldValue, float newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.FireRateCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.FireRateCounter, newValue, oldValue));
        }

        private void UpdateDamageCostCounter(int oldValue, int newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.DamageUpgradeCostCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.DamageUpgradeCostCounter, newValue, oldValue));
        }

        private void UpdateFireRateCostCounter(int oldValue, int newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.FireRateUpgradeCostCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.FireRateUpgradeCostCounter, newValue, oldValue));
        }

        private void UpdateDamageLevelCounter(int oldValue, int newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.DamageLevelCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.DamageLevelCounter, newValue, oldValue));
        }

        private void UpdateFireRateLevelCounter(int oldValue, int newValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(_config.FireRateLevelCounter, newValue, oldValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(_config.FireRateLevelCounter, newValue, oldValue));
        }

        private IEnumerator SmoothCounter(TMP_Text counter, int endValue, int startValue)
        {
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime / _config.CountersSmooth;

                counter.text = Convert.ToInt32(Mathf.Lerp(startValue, endValue, time)).ToString();

                yield return new WaitForEndOfFrame();
            }

            counter.text = endValue.ToString();
        }

        private IEnumerator SmoothCounter(TMP_Text counter, float endValue, float startValue)
        {
            float time = 0f;

            while (time < 1f)
            {
                time += Time.deltaTime / _config.CountersSmooth;

                counter.text = Mathf.Lerp(startValue, endValue, time).ToString();

                yield return new WaitForEndOfFrame();
            }

            counter.text = endValue.ToString();
        }
    }
}
