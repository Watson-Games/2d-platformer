using Assets.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var tryJumpFilter = world.Filter<TryJumpComponent>().End();
            var playerPool = world.GetPool<PlayerComponent>();

            foreach (var tryJump in tryJumpFilter)
            {
                world.DelEntity(tryJump);

                foreach (var entity in filter)
                {
                    ref var playerComponent = ref playerPool.Get(entity);

                    if (IsGrounded(playerComponent.Collider, playerComponent.JumpableLayers))
                    {
                        playerComponent.Rigidbody.velocity =
                            new Vector2(playerComponent.Rigidbody.velocity.x, playerComponent.JumpForce);
                    }
                }
            }
        }

        private static bool IsGrounded(Collider2D collider, LayerMask jumpableLayers)
        {
            var bounds = collider.bounds;

            return Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, 0.1f,
                jumpableLayers);
        }
    }
}