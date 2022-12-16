using Sources.Providers;
using UnityEngine;

namespace Sources.Ecs
{
    internal struct ZombieMoveConfig
    {
        public ZombieProvider Provider;

        public float Smooth;
        public float MaxSpeed;

        public float CurrentFireRate;
        public float AttackFireRate;

        public float StopDistance;

        public Vector3 Velosity;
    }
}
