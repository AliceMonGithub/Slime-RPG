using Lean.Transition;
using UnityEngine;

namespace Sources.Behaviour
{
    internal class DamagePopupBehaviour : MonoBehaviour
    {
        [SerializeField] private LeanManualAnimation _animation;

        public void Awake()
        {
            _animation.BeginTransitions();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
