using Assets.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerInputComponent>().End();
            var playerInputPool = world.GetPool<PlayerInputComponent>();
            var tryJumpPool = world.GetPool<TryJumpComponent>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);
                var directionX = Input.GetAxis("Horizontal");
                var directionY = Input.GetAxis("Vertical");
                playerInputComponent.MoveVector = new Vector2(directionX, directionY);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    var tryJump = world.NewEntity();
                    tryJumpPool.Add(tryJump);
                }
            }
        }
    }
}
