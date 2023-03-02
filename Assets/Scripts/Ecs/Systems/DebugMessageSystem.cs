using System;
using Ecs.Components.Requests;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class DebugMessageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DebugMessageRequest> _messageFilter = null;
        
        public void Run()
        {
            foreach (var entityId in _messageFilter)
            {
                ref var messageEvent = ref _messageFilter.Get1(entityId);

                switch (messageEvent.Type)
                {
                    case MessageType.Log:     Debug.Log(messageEvent.Message);        break;
                    case MessageType.Warning: Debug.LogWarning(messageEvent.Message); break;
                    case MessageType.Error:   Debug.LogError(messageEvent.Message);   break;
                    default: throw new ArgumentOutOfRangeException();
                }
                
                _messageFilter.GetEntity(entityId).Del<DebugMessageRequest>();
            }
        }
    }
}