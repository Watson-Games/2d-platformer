using Assets.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
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
                var movementVector = playerInputComponent.MoveVector;

                playerComponent.Rigidbody.velocity =
                    new Vector2(movementVector.x * playerComponent.Speed, playerComponent.Rigidbody.velocity.y);
            }
        }
    }
}