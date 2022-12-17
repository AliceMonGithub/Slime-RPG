using UnityEngine;

namespace Sources.Providers
{
    internal class TileProvider : MonoBehaviour
    {
        [SerializeField] private Transform _cameraPoint;

        [Space]

        [SerializeField] private Transform _root;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _startPoint;

        public Transform CameraPoint => _cameraPoint;

        public Transform Root => _root;
        public Transform EndPoint => _endPoint;
        public Transform StartPoint => _startPoint;
    }
}
