using DG.Tweening;
using Leopotam.EcsLite;
using Sources.Providers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Ecs
{
    internal class TilesSpawning : IEcsInitSystem
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private List<TileProvider> _spawnedTiles;

        private TileProvider[] _tiles;
        private TileProvider _currentTile;

        public TilesSpawning(TileProvider[] tiles, TileProvider currentTile, ICoroutineRunner coroutineRunner)
        {
            _spawnedTiles = new List<TileProvider>();

            _tiles = tiles;
            _currentTile = currentTile;

            _spawnedTiles.Add(_currentTile);

            _coroutineRunner = coroutineRunner;
        }

        public void Init(IEcsSystems systems)
        {
            //SpawnTile();

            //_coroutineRunner.InvokeCoroutine(SpawnLoop());
        }

        //private IEnumerator SpawnLoop()
        //{
        //    while(true)
        //    {
        //        yield return new WaitForSeconds(6f);

        //        SpawnTile();
        //    }
        //}

        //private void SpawnTile()
        //{
        //    TileProvider newTile = _tiles[Random.Range(0, _tiles.Length)];

        //    TileProvider instance = Object.Instantiate(newTile, _currentTile.EndPoint.position - newTile.StartPoint.localPosition, Quaternion.identity);
        //    _currentTile = instance;

        //    _spawnedTiles.Add(instance);

        //    if(_spawnedTiles.Count >= 3)
        //    {
        //        Object.DestroyImmediate(_spawnedTiles[0].gameObject, true);

        //        _spawnedTiles.RemoveAt(0);
        //    }

        //    Vector3 oldPosition = _spawnedTiles[0].Root.position;

        //    _spawnedTiles[0].Root.DOMove(_spawnedTiles[0].EndPoint.position - instance.EndPoint.localPosition, 3);
        //    _spawnedTiles[1].Root.DOMove(oldPosition, 3);
        //}
    }
}
