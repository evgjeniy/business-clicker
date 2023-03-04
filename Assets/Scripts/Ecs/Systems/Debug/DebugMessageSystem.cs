using System;
using Ecs.Components.Requests;
using Leopotam.Ecs;

namespace Ecs.Systems.Debug
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
                    case MessageType.Log:     UnityEngine.Debug.Log(messageEvent.message);        break;
                    case MessageType.Warning: UnityEngine.Debug.LogWarning(messageEvent.message); break;
                    case MessageType.Error:   UnityEngine.Debug.LogError(messageEvent.message);   break;
                    default: throw new ArgumentOutOfRangeException();
                }
                
                _messagesFilter.GetEntity(entityId).Del<DebugMessageRequest>();
            }
        }
    }
}