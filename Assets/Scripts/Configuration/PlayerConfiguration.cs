using UnityEngine;

namespace Assets.Scripts.Configuration
{
    [CreateAssetMenu(fileName = "Player Configuration")]
    public class PlayerConfiguration : ScriptableObject
    {
        public float JumpForce;
        public float Speed;
        public LayerMask JumpableLayers;
    }
}