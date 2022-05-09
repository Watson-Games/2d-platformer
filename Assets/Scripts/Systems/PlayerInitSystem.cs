using Assets.Scripts.Components;
using Assets.Scripts.Configuration;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerConfiguration = systems.GetShared<GameConfiguration>().PlayerConfiguration;
            var playerEntity = world.NewEntity();

            var playerPool = world.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);

            var playerInputPool = world.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);

            var playerGameObject = GameObject.FindGameObjectWithTag("Player");
            playerComponent.Transform = playerGameObject.transform;
            playerComponent.Rigidbody = playerGameObject.GetComponent<Rigidbody2D>();
            playerComponent.Collider = playerGameObject.GetComponent<BoxCollider2D>();
            playerComponent.Animator = playerGameObject.GetComponent<Animator>();
            playerComponent.SpriteRenderer = playerGameObject.GetComponent<SpriteRenderer>();
            playerComponent.JumpForce = playerConfiguration.JumpForce;
            playerComponent.Speed = playerConfiguration.Speed;
            playerComponent.JumpableLayers = playerConfiguration.JumpableLayers;
        }
    }
}
