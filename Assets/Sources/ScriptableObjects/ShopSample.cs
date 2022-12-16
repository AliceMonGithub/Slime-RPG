using Sources.Properties;
using System;
using UnityEngine;

namespace Sources.Properties
{
    internal interface IShop
    {
        public Action<int, int> OnDamageUpgradeCostChanged { get; set; }
        public Action<int, int> OnFireRateUpgradeCostChanged { get; set; }

        public Action<int, int> OnDamageUpgradeValueChanged { get; set; }
        public Action<float, float> OnFireRateUpgradeValueChanged { get; set; }

        public Action<int, int> OnDamageLevelChanged { get; set; }
        public Action<int, int> OnFireRateLevelChanged { get; set; }
        public Action<int, int> OnMaxHealthUpgradeCostChanged { get; set; }
        public Action<int, int> OnMaxHealthUpgradeValueChanged { get; set; }
        public Action<int, int> OnMaxHealthLevelChanged { get; set; }

        public int DamageUpgradeIncrease { get; }
        public int FireRateUpgradeIncrease { get; }
        public int MaxHealthUpgradeIncrease { get; }

        public int StartDamageUpgradeCost { get; }
        public int StartFireRateUpgradeCost { get; }
        public int StartMaxHealthUpgradeCost { get; }

        public int StartDamageUpgradeValue{ get; }
        public float StartFireRateUpgradeValue { get; }
        public int StartMaxHealthUpgradeValue { get; }

        public int StartDamageLevel { get; }
        public int StartFireRateLevel { get; }
        public int StartMaxHealthLevel { get; }

        public int DamageUpgradeCost { get; }
        public int FireRateUpgradeCost { get; }
        public int MaxHealthUpgradeCost { get; }

        public int DamageUpgradeValue { get; }
        public float FireRateUpgradeValue { get; }
        public int MaxHealthUpgradeValue { get; }

        public int DamageLevel { get; }
        public int FireRateLevel { get; }
        public int MaxHealthLevel { get; }

        public void IncreaseDamageUpgradeCost(int value);
        public void IncreaseFireRateUpgradeCost(int value);
        public void IncreaseMaxHealthUpgradeCost(int value);

        public void IncreaseDamageUpgradeValue(int value);
        public void IncreaseFireRateUpgradeValue(float value);
        public void IncreaseMaxHealthUpgradeValue(int value);

        public void IncreaseDamageLevel(int value);
        public void IncreaseFireRateLevel(int value);
        public void IncreaseMaxHealthLevel(int value);
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class ShopSample : ScriptableObject, IShop
    {
        [SerializeField] private int _damageUpgradeIncrease;
        [SerializeField] private int _fireRateUpgradeIncrease;
        [SerializeField] private int _maxHealthUpgradeIncrease;

        [Space]

        [SerializeField] private int _startDamageUpgradeCost;
        [SerializeField] private int _startFireRateUpradeCost;
        [SerializeField] private int _startMaxHealthUpgradeCost;

        [Space]

        [SerializeField] private int _startDamageUpgradeValue;
        [SerializeField] private float _startFireRateUpgradeValue;
        [SerializeField] private int _startMaxHealthUpgradeValue;

        [Space]

        [SerializeField] private int _startDamageLevel;
        [SerializeField] private int _startFireRateLevel;
        [SerializeField] private int _startMaxHealthLevel;

        [NonSerialized] private int _damageUpgradeCost;
        [NonSerialized] private int _fireRateUpgradeCost;
        [NonSerialized] private int _maxHealthUpgradeCost;

        [NonSerialized] private int _damageUpgradeValue;
        [NonSerialized] private float _fireRateUpgradeValue;
        [NonSerialized] private int _maxHealthUpgradeValue;

        [NonSerialized] private int _damageLevel;
        [NonSerialized] private int _fireRateLevel;
        [NonSerialized] private int _maxHealthLevel;

        public Action<int, int> OnDamageUpgradeCostChanged { get; set; }
        public Action<int, int> OnFireRateUpgradeCostChanged { get; set; }
        public Action<int, int> OnDamageUpgradeValueChanged { get; set; }
        public Action<float, float> OnFireRateUpgradeValueChanged { get; set; }
        public Action<int, int> OnDamageLevelChanged { get; set; }
        public Action<int, int> OnFireRateLevelChanged { get; set; }
        public Action<int, int> OnMaxHealthUpgradeCostChanged { get; set; }
        public Action<int, int> OnMaxHealthUpgradeValueChanged { get; set; }
        public Action<int, int> OnMaxHealthLevelChanged { get; set; }

        public int DamageUpgradeIncrease => _damageUpgradeIncrease;
        public int FireRateUpgradeIncrease => _fireRateUpgradeIncrease;

        public int StartDamageUpgradeCost => _startDamageUpgradeCost;
        public int StartFireRateUpgradeCost => _startFireRateUpradeCost;

        public int StartDamageUpgradeValue => _startDamageUpgradeValue;
        public float StartFireRateUpgradeValue => _startFireRateUpgradeValue;

        public int StartDamageLevel => _startDamageLevel;
        public int StartFireRateLevel => _startFireRateLevel;

        public int DamageUpgradeCost => _damageUpgradeCost;
        public int FireRateUpgradeCost => _fireRateUpgradeCost;

        public int DamageUpgradeValue => _damageUpgradeValue;
        public float FireRateUpgradeValue => _fireRateUpgradeValue;

        public int DamageLevel => _damageLevel;
        public int FireRateLevel => _fireRateLevel;

        public int MaxHealthUpgradeIncrease => _maxHealthUpgradeIncrease;
        public int StartMaxHealthUpgradeCost => _startMaxHealthUpgradeCost;
        public int StartMaxHealthUpgradeValue => _startMaxHealthUpgradeValue;
        public int StartMaxHealthLevel => _startMaxHealthLevel;
        public int MaxHealthUpgradeCost => _maxHealthUpgradeCost;
        public int MaxHealthUpgradeValue => _maxHealthUpgradeValue;
        public int MaxHealthLevel => _maxHealthLevel;

        public void IncreaseDamageUpgradeCost(int value)
        {
            OnDamageUpgradeCostChanged?.Invoke(_damageUpgradeCost, _damageUpgradeCost + value);

            _damageUpgradeCost += value;
        }

        public void IncreaseFireRateUpgradeCost(int value)
        {
            OnFireRateUpgradeCostChanged?.Invoke(_fireRateUpgradeCost, _fireRateUpgradeCost + value);

            _fireRateUpgradeCost += value;
        }

        public void IncreaseDamageUpgradeValue(int value)
        {
            OnDamageUpgradeValueChanged?.Invoke(_damageUpgradeValue, _damageUpgradeValue + value);

            _damageUpgradeValue += value;
        }

        public void IncreaseFireRateUpgradeValue(float value)
        {
            OnFireRateUpgradeValueChanged?.Invoke(_fireRateUpgradeValue, _fireRateUpgradeValue + value);

            _fireRateUpgradeValue += value;
        }

        public void IncreaseDamageLevel(int value)
        {
            OnDamageLevelChanged?.Invoke(_damageLevel, _damageLevel + value);

            _damageLevel += value;
        }

        public void IncreaseFireRateLevel(int value)
        {
            OnFireRateLevelChanged?.Invoke(_fireRateLevel, _fireRateLevel + value);

            _fireRateLevel += value;
        }

        public void IncreaseMaxHealthUpgradeCost(int value)
        {
            OnMaxHealthUpgradeCostChanged?.Invoke(_maxHealthUpgradeCost, _maxHealthUpgradeCost + value);

            _maxHealthUpgradeCost += value;
        }

        public void IncreaseMaxHealthUpgradeValue(int value)
        {
            OnMaxHealthUpgradeValueChanged?.Invoke(_maxHealthUpgradeValue, _maxHealthUpgradeValue + value);

            _maxHealthUpgradeValue += value;
        }

        public void IncreaseMaxHealthLevel(int value)
        {
            OnMaxHealthLevelChanged?.Invoke(_startMaxHealthLevel, _maxHealthLevel + value);

            _maxHealthLevel += value;
        }
    }
}
