using Assets.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private static readonly int animatorMovementStateId = Animator.StringToHash("movementState");

        private enum MovementState
        {
            Idle = 0,
            Running = 1,
            Jumping = 2,
            Falling = 3
        }

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var playerPool = world.GetPool<PlayerComponent>();
            var playerInputPool = world.GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                var directionX = playerInputComponent.MoveVector.x;
                var rigidbody2D = playerComponent.Rigidbody;

                var isRunning = directionX != 0;
                var movementState = GetMovementState(rigidbody2D.velocity.y, isRunning);
                playerComponent.Animator.SetInteger(animatorMovementStateId, (int) movementState);

                if (isRunning)
                {
                    playerComponent.SpriteRenderer.flipX = directionX < 0;
                }
            }
        }

        private static MovementState GetMovementState(float verticalVelocity, bool isRunning)
        {
            var isFloating = Mathf.Abs(verticalVelocity) > 0.1;

            if (isFloating)
            {
                return verticalVelocity > 0 ? MovementState.Jumping : MovementState.Falling;
            }

            return isRunning ? MovementState.Running : MovementState.Idle;
        }
    }
}
