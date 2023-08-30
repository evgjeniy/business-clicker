using System;
using Ecs.Components.Requests;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.DebugMessaging
{
    public class DebugMessageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DebugMessageRequest> _messagesFilter = null;
        
        public void Run()
        {
            foreach (var entityId in _messagesFilter)
            {
                ref var messageEvent = ref _messagesFilter.Get1(entityId);

                switch (messageEvent.type)
                {
                    case MessageType.Log:     Debug.Log(messageEvent.message);        break;
                    case MessageType.Warning: Debug.LogWarning(messageEvent.message); break;
                    case MessageType.Error:   Debug.LogError(messageEvent.message);   break;
                    default: throw new ArgumentOutOfRangeException();
                }
                
                _messagesFilter.GetEntity(entityId).Del<DebugMessageRequest>();
            }
        }
    }
}