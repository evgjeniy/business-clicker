using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems.InitializeSystems
{
    public class BusinessViewsSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessNamesDb _namesDb = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<TransformComponent, PrefabComponent> _spawnerFilter = null;

        public void Init()
        {
            foreach (var entityId in _spawnerFilter)
            {
                ref var entity = ref _spawnerFilter.GetEntity(entityId);
                ref var parentTransform = ref entity.Get<TransformComponent>().Transform;
                ref var prefab = ref entity.Get<PrefabComponent>().prefab;

                if (_namesDb.Size == _configDb.Size)
                {
                    for (var i = 0; i < _namesDb.Size; i++)
                        Object.Instantiate(prefab, parentTransform).transform.SetSiblingIndex(i);
                }
                else
                {
                    SendDatabasesCountNotEqualsError();
                }

                entity.Del<TransformComponent>();
                entity.Del<PrefabComponent>();
            }
        }

        private void SendDatabasesCountNotEqualsError()
        {
            _world.SendMessage(new DebugMessageRequest
            {
                type = MessageType.Error,
                message = "BusinessViewsEcsSpawner.Init() the method cannot work correctly, " +
                          "because the sizes of the business databases are not equal."
            });
        }
    }
}