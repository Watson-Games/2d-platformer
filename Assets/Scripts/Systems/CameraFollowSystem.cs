using Assets.Scripts.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int cameraEntity;

        public void Init(EcsSystems systems)
        {
            cameraEntity = systems.GetWorld().NewEntity();
            var cameraPool = systems.GetWorld().GetPool<CameraComponent>();
            cameraPool.Add(cameraEntity);
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            cameraComponent.CameraTransform = Camera.main.transform;
        }

        public void Run(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<PlayerComponent>().End();
            var playerPool = world.GetPool<PlayerComponent>();
            var cameraPool = world.GetPool<CameraComponent>();

            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);

                var currentCameraPosition = cameraComponent.CameraTransform.position;
                var currentPlayerPosition = playerComponent.Transform.position;

                cameraComponent.CameraTransform.position = new Vector3(
                    currentPlayerPosition.x,
                    currentPlayerPosition.y,
                    currentCameraPosition.z);
            }
        }
    }
}