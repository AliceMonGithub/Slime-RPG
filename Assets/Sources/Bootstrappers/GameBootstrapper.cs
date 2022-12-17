using Leopotam.EcsLite;
using Sources.Ecs;
using Sources.Factories;
using Sources.Providers;
using Sources.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform[] _zombieSpawnPoints;

        [Space]

        [SerializeField] private ZombieSample _zombieSample;
        [SerializeField] private SlimeSample _slimeSample;
        [SerializeField] private ShopSample _shopSample;

        [SerializeField] private PlayerStats _playerStats;

        [Space]

        [SerializeField] private TileProvider[] _tiles;
        [SerializeField] private TileProvider _startTile;

        [Space]

        [SerializeField] private ShopUIConfig _shopUIConfig;
        [SerializeField] private TMP_Text _coinsCounter;
        [SerializeField] private float _counterSmooth;
        [SerializeField] private Image _slimeHealthBar;

        [Space]

        [SerializeField] private Transform _slimeTransform;
        [SerializeField] private Transform _shotPoint;

        private EcsWorld _world;

        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);

            _systems
                .Add(new SlimeInit(_slimeSample, _slimeTransform, _shotPoint))
                .Add(new SliderHealthBar(_slimeHealthBar, _slimeSample, new PopupFactory(_zombieSample.DamagePopup)))
                .Add(new CoinsCounter(_playerStats, _counterSmooth, _coinsCounter, this))
                .Add(new ShopUI(_shopUIConfig, _playerStats, _shopSample, _slimeSample, this))
                .Add(new ZombiesSpawn(new ZombieFactory(_zombieSample.Prefab), _slimeTransform, _zombieSpawnPoints, _playerStats, _zombieSample, this))
                .Add(new ZombieMovement(_world, _slimeTransform, _zombieSample))
                .Add(new SlimeShoting(_world, _slimeSample, _slimeTransform, new BulletFactory(_slimeSample.BulletPrefab)))
                .Add(new BulletMovement())
                .Add(new TilesSpawning(_tiles, _startTile, this))
                .Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

            _systems.Destroy();
            _systems = null;

            _world.Destroy();
            _world = null;
        }

        Coroutine ICoroutineRunner.InvokeCoroutine(System.Collections.IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        void ICoroutineRunner.StopCoroutine(System.Collections.IEnumerator enumerator)
        {
            StopCoroutine(enumerator);
        }

        void ICoroutineRunner.Invoke(string name, float time)
        {
            Invoke(name, time);
        }

        void ICoroutineRunner.InvokeRepeating(string name, float time, float rate)
        {
            InvokeRepeating(name, time, rate);
        }
    }
}
