using UnityEngine;
using UnityEngine.UI;

namespace Sources.Providers
{
    internal class ZombieProvider : MonoBehaviour
    {
        [SerializeField] private GameObject _barCanvas;
        [SerializeField] private Transform _sliderTransform;
        [SerializeField] private Image _barSlider;

        [Space]

        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _root;
        [SerializeField] private Animator _animator;

        public GameObject BarCanvas => _barCanvas;
        public Transform SliderTransform => _sliderTransform;
        public Image BarSlider => _barSlider;

        public Transform Transform => _transform;
        public Transform Root => _root;
        public Animator Animator => _animator;
    }
}
