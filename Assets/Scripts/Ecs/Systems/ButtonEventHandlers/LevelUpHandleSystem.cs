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
    public class LevelUpHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<RootTransformComponent, LevelUpButtonTag, OnButtonClickEvent> _levelUpRequestsFilter = null;

        public void Run()
        {
            foreach (var entityId in _levelUpRequestsFilter)
            {
                ref var entity = ref _levelUpRequestsFilter.GetEntity(entityId);
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.NextLevelPrice });
                businessConfig.Level += 1;

                _world.AddComponentByTag<LevelUpPriceTextTag, UpdateViewEvent>(businessIndex);
                _world.AddComponentByTag<RevenueTextTag, UpdateViewEvent>(businessIndex);
                _world.AddComponentByTag<LevelNumberTextTag, UpdateViewEvent>(businessIndex);
                entity.Del<OnButtonClickEvent>();
            }
        }
    }
}