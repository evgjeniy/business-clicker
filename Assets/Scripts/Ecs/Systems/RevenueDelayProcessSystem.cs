using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.UiComponents;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Ecs.Systems
{
    public class RevenueDelayProcessSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<RevenueProcessComponent, RootTransformComponent> _revenueProcessFilter = null;

        public void Run()
        {
            foreach (var entityId in _revenueProcessFilter)
            {
                ref var entity = ref _revenueProcessFilter.GetEntity(entityId);
                ref var uiSlider = ref entity.Get<RevenueProcessComponent>().uiSlider;
                
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(index);
                
                if (entity.Has<InitializeEvent>()) Initialize(ref entity, uiSlider, businessConfig);
                if (businessConfig.Level == 0) continue;

                IncreaseDeltaTime(uiSlider, businessConfig.GetCurrentRevenue());
            }
        }

        private void IncreaseDeltaTime(Slider uiSlider, float currentRevenue)
        {
            uiSlider.value += Time.deltaTime;
            if (uiSlider.value < uiSlider.maxValue) return;

            uiSlider.value = uiSlider.minValue;
            
            _world.SendMessage(new DebugMessageRequest
            {
                Type = MessageType.Log,
                Message = $"The balance was replenished by {currentRevenue}$"
            });
            
            // TODO - _world.SendMessage(new ReplenishBalanceRequest { value = currentRevenue });
        }

        private void Initialize(ref EcsEntity entity, Slider uiSlider, BusinessConfig businessConfig)
        {
            uiSlider.maxValue = businessConfig.RevenueDelay;
            entity.Del<InitializeEvent>();
        }
    }
}