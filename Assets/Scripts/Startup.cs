using Assets.Scripts.Configuration;
using Assets.Scripts.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Startup : MonoBehaviour
    {
        private EcsWorld world;
        private EcsSystems initSystems;
        private EcsSystems updateSystems;
        private EcsSystems lateUpdateSystems;

        [SerializeField] private PlayerConfiguration playerConfiguration;

        private void Start()
        {
            world = new EcsWorld();

            var gameConfiguration = new GameConfiguration
            {
                PlayerConfiguration = playerConfiguration
            };

            initSystems = new EcsSystems(world, gameConfiguration);
            initSystems
                .Add(new PlayerInitSystem())
                .Init();

            updateSystems = new EcsSystems(world, gameConfiguration);
            updateSystems
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerJumpSystem())
                .Init();

            lateUpdateSystems = new EcsSystems(world, gameConfiguration);
            lateUpdateSystems
                .Add(new CameraFollowSystem())
                .Add(new PlayerAnimationSystem())
                .Init();
        }

        private void Update()
        {
            updateSystems?.Run();
        }

        private void LateUpdate()
        {
            lateUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            initSystems?.Destroy();
            updateSystems?.Destroy();
            lateUpdateSystems?.Destroy();
            world?.Destroy();
        }
    }
}