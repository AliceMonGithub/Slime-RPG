using UnityEngine;

namespace Sources.Ecs
{
    internal struct Bullet
    {
        public Transform Target;
        

        public int Damage;

        public float MaxSpeed;
        public float Smooth;

        public float DamageDistance;

        public Vector3 Velosity;
    }
}