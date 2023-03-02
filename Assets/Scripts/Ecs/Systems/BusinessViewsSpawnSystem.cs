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
                ref var parentTransform = ref _spawnerFilter.Get1(entityId).transform;
                ref var prefab = ref _spawnerFilter.Get2(entityId).prefab;

                if (_businessNamesDb.Count == _businessConfigDb.Count)
                {
                    for (var i = 0; i < _businessNamesDb.Count; i++)
                        Instantiate(prefab, parentTransform, i);
                }
                else
                {
                    SendDatabasesCountNotEqualsError();
                }

                RemoveEntity(ref _spawnerFilter.GetEntity(entityId));
            }
        }

        private void Instantiate(GameObject prefab, Transform parent, int siblingIndex)
        {
            var newInstance = Object.Instantiate(prefab, parent);
            
            _world.SendMessage(new BusinessViewSetupRequest
            {
                SiblingIndex = siblingIndex,
                BusinessName = _businessNamesDb.GetById(siblingIndex),
                BusinessConfig = _businessConfigDb.GetById(siblingIndex)
            });
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

        private void RemoveEntity(ref EcsEntity entity)
        {
            entity.Del<TransformComponent>();
            entity.Del<PrefabComponent>();
        }
    }
}