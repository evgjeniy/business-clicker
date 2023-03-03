using Ecs.Components;
using Ecs.Components.Requests;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class BusinessViewsSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessNamesDb _businessNamesDb = null;
        private readonly BusinessConfigDb _businessConfigDb = null;
        private readonly EcsFilter<TransformComponent, PrefabComponent> _spawnerFilter = null;

        public void Init()
        {
            foreach (var entityId in _spawnerFilter)
            {
                ref var entity = ref _spawnerFilter.GetEntity(entityId);
                ref var parentTransform = ref entity.Get<TransformComponent>().Transform;
                ref var prefab = ref entity.Get<PrefabComponent>().prefab;

                if (_businessNamesDb.Count == _businessConfigDb.Count)
                {
                    for (var i = 0; i < _businessNamesDb.Count; i++)
                        Object.Instantiate(prefab, parentTransform).transform.SetSiblingIndex(i);
                }
                else
                {
                    SendDatabasesCountNotEqualsError();
                }

                entity.Del<TransformComponent>();
                entity.Del<PrefabComponent>();

                Debug.Log(entity.IsAlive() + " || " + entity.IsNull() + " || " + entity.IsWorldAlive());
            }
        }

        private void SendDatabasesCountNotEqualsError()
        {
            _world.SendMessage(new DebugMessageRequest
            {
                Type = MessageType.Error,
                Message = "BusinessViewsEcsSpawner.Init() method can't work correctly, " +
                          "cause the sizes of the 2 databases are not equals."
            });
        }
    }
}