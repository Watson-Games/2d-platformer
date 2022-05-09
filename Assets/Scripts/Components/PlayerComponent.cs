using UnityEngine;

namespace Assets.Scripts.Components
{
    public struct PlayerComponent
    {
        public Transform Transform;
        public Rigidbody2D Rigidbody;
        public BoxCollider2D Collider;
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        public float JumpForce;
        public float Speed;
        public LayerMask JumpableLayers;
    }
}
