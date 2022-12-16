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

        public int DamageUpgradeIncrease { get; }
        public int FireRateUpgradeIncrease { get; }

        public int StartDamageUpgradeCost { get; }
        public int StartFireRateUpgradeCost { get; }

        public int StartDamageUpgradeValue{ get; }
        public float StartFireRateUpgradeValue { get; }

        public int StartDamageLevel { get; }
        public int StartFireRateLevel { get; }

        public int DamageUpgradeCost { get; }
        public int FireRateUpgradeCost { get; }

        public int DamageUpgradeValue { get; }
        public float FireRateUpgradeValue { get; }

        public int DamageLevel { get; }
        public int FireRateLevel { get; }

        public void IncreaseDamageUpgradeCost(int value);
        public void IncreaseFireRateUpgradeCost(int value);

        public void IncreaseDamageUpgradeValue(int value);
        public void IncreaseFireRateUpgradeValue(float value);

        public void IncreaseDamageLevel(int value);
        public void IncreaseFireRateLevel(int value);
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class ShopSample : ScriptableObject, IShop
    {
        [SerializeField] private int _damageUpgradeIncrease;
        [SerializeField] private int _fireRateUpgradeIncrease;

        [Space]

        [SerializeField] private int _startDamageUpgradeCost;
        [SerializeField] private int _startFireRateUpradeCost;

        [Space]

        [SerializeField] private int _startDamageUpgradeValue;
        [SerializeField] private float _startFireRateUpgradeValue;

        [Space]

        [SerializeField] private int _startDamageLevel;
        [SerializeField] private int _startFireRateLevel;

        [NonSerialized] private int _damageUpgradeCost;
        [NonSerialized] private int _fireRateUpgradeCost;

        [NonSerialized] private int _damageUpgradeValue;
        [NonSerialized] private float _fireRateUpgradeValue;

        [NonSerialized] private int _damageLevel;
        [NonSerialized] private int _fireRateLevel;

        public Action<int, int> OnDamageUpgradeCostChanged { get; set; }
        public Action<int, int> OnFireRateUpgradeCostChanged { get; set; }
        public Action<int, int> OnDamageUpgradeValueChanged { get; set; }
        public Action<float, float> OnFireRateUpgradeValueChanged { get; set; }
        public Action<int, int> OnDamageLevelChanged { get; set; }
        public Action<int, int> OnFireRateLevelChanged { get; set; }

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
    }
}
