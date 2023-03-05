using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.Tags.Buttons;
using Ecs.Components.Tags.Texts;
using Ecs.Utilities;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.ButtonEventHandlers
{
    public class FirstUpgradeHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<RootTransformComponent, FirstUpgradeButtonTag, OnButtonClickEvent> _firstUpgradeRequestsFilter = null;
        
        public void Run()
        {
            foreach (var entityId in _firstUpgradeRequestsFilter)
            {
                ref var entity = ref _firstUpgradeRequestsFilter.GetEntity(entityId);
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.FirstUpgrade.Price });
                businessConfig.FirstUpgrade.IsPurchased = true;

                _world.AddComponentByTag<FirstUpgradePriceTag, UpdateViewEvent>(businessIndex);
                _world.AddComponentByTag<RevenueTextTag, UpdateViewEvent>(businessIndex);
                entity.Del<OnButtonClickEvent>();
            }
        }
    }
}