using Leopotam.EcsLite;
using Sources.Properties;
using Sources.ScriptableObjects;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Sources.Ecs
{
    internal class CoinsCounter : IEcsInitSystem
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly IPlayerStats _playerStats;
        private readonly TMP_Text _counter;
        private readonly float _counterSmooth;

        public CoinsCounter(IPlayerStats playerStats, float counterSmooth, TMP_Text counter, ICoroutineRunner coroutineRunner)
        {
            _playerStats = playerStats;
            _counter = counter;
            _counterSmooth = counterSmooth;

            _coroutineRunner = coroutineRunner;
        }

        public void Init(IEcsSystems systems)
        {
            _playerStats.OnCoinsValueChanged += InvokeCounter;

            _playerStats.IncreaseCoins(_playerStats.StartCoins);
        }

        private void InvokeCounter(int endValue, int startValue)
        {
            _coroutineRunner.StopCoroutine(SmoothCounter(startValue, endValue));

            _coroutineRunner.InvokeCoroutine(SmoothCounter(startValue, endValue));
        }

        private IEnumerator SmoothCounter(int endValue, int startValue)
        {
            float time = 0f;

            while(time < 1f)
            {
                time += Time.deltaTime / _counterSmooth;

                _counter.text = Convert.ToInt32(Mathf.Lerp(startValue, endValue, time)).ToString();

                yield return new WaitForEndOfFrame();
            }

            _counter.text = endValue.ToString();
        }
    }
}
