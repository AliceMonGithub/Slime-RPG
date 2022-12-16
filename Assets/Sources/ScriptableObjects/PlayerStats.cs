using Sources.Properties;
using System;
using UnityEngine;

namespace Sources.Properties
{
    internal interface IPlayerStats
    {
        public Action<int, int> OnCoinsValueChanged { get; set; }

        public int Coins { get; }
        public int StartCoins { get; }

        public void IncreaseCoins(int value);
        public void DecreaseCoins(int value);
    }
}

namespace Sources.ScriptableObjects
{
    [CreateAssetMenu]
    internal class PlayerStats : ScriptableObject, IPlayerStats
    {
        [SerializeField] private int _startCoins;

        [NonSerialized] private int _coins;

        public Action<int, int> OnCoinsValueChanged { get; set; }

        public int Coins => _coins;
        public int StartCoins => _startCoins;

        public void IncreaseCoins(int value)
        {
            OnCoinsValueChanged?.Invoke(_coins, _coins + value);

            _coins += value;
        }

        public void DecreaseCoins(int value)
        {
            OnCoinsValueChanged?.Invoke(_coins, _coins - value);

            _coins -= value;
        }
    }
}
