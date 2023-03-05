﻿using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.UiComponents;
using Ecs.Utilities;
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
        private readonly EcsFilter<SliderComponent, RootTransformComponent> _revenueProcessesFilter = null;

        public void Run()
        {
            foreach (var entityId in _revenueProcessesFilter)
            {
                ref var entity = ref _revenueProcessesFilter.GetEntity(entityId);
                ref var uiSlider = ref entity.Get<SliderComponent>().uiSlider;
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(businessIndex);
                
                if (entity.Has<InitializeEvent>()) 
                    Initialize(ref entity, uiSlider, businessConfig.RevenueDelay);
                
                if (businessConfig.Level == 0) continue;

                IncreaseDeltaTime(businessConfig);

                uiSlider.value = businessConfig.CurrentProcess;
            }
        }

        private void Initialize(ref EcsEntity entity, Slider uiSlider, float revenueDelay)
        {
            uiSlider.maxValue = revenueDelay;
            entity.Del<InitializeEvent>();
        }

        private void IncreaseDeltaTime(BusinessConfig businessConfig)
        {
            businessConfig.CurrentProcess += Time.deltaTime;
            if (businessConfig.CurrentProcess < businessConfig.RevenueDelay) return;

            businessConfig.CurrentProcess = 0;
            _world.SendMessage(new ReplenishBalanceRequest { value = businessConfig.GetCurrentRevenue() });
        }
    }
}