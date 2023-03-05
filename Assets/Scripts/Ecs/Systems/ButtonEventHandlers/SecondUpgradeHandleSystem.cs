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
    public class SecondUpgradeHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<RootTransformComponent, SecondUpgradeButtonTag, OnButtonClickEvent> _secondUpgradeRequestsFilter = null;
        
        public void Run()
        {
            foreach (var entityId in _secondUpgradeRequestsFilter)
            {
                ref var entity = ref _secondUpgradeRequestsFilter.GetEntity(entityId);
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.secondUpgrade.price });
                businessConfig.secondUpgrade.isPurchased = true;

                _world.AddComponentByTag<SecondUpgradePriceTag, UpdateViewEvent>(businessIndex);
                _world.AddComponentByTag<RevenueTextTag, UpdateViewEvent>(businessIndex);
                entity.Del<OnButtonClickEvent>();
            }
        }
    }
}