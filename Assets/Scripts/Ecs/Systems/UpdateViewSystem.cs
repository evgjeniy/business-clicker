using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class UpdateViewSystem : IEcsRunSystem
    {
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<UpdateViewEvent> _updateViewEventsFilter = null;
        private readonly EcsFilter<TextComponent, RootTransformComponent> _textsForUpdateFilter = null;

        public void Run()
        {
            if (!IsUpdateEventExist()) return;

            foreach (var entityId in _textsForUpdateFilter)
            {
                ref var entity = ref _textsForUpdateFilter.GetEntity(entityId);
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();

                UpdateView(ref entity, _configDb.GetById(index));
            }
        }

        private void UpdateView(ref EcsEntity entity, BusinessConfig config)
        {
            ref var uiText = ref entity.Get<TextComponent>().uiText;

            if (entity.Has<FirstUpgradeRevenueTag>())
                uiText.text = $"Revenue: +{config.FirstUpgrade.RevenueMultiplier * 100}$";
            else if (entity.Has<FirstUpgradePriceTag>())
                uiText.text = $"Price: {config.FirstUpgrade.Price}$";
            else if (entity.Has<SecondUpgradeRevenueTag>())
                uiText.text = $"Revenue: +{config.SecondUpgrade.RevenueMultiplier * 100}$";
            else if (entity.Has<SecondUpgradePriceTag>())
                uiText.text = $"Price: {config.SecondUpgrade.Price}$";
            else if (entity.Has<LevelTextTag>())
                uiText.text = $"LVL: {config.Level}";
            else if (entity.Has<RevenueTextTag>())
                uiText.text = $"Revenue: {config.GetCurrentRevenue()}$";
            else if (entity.Has<LevelUpTextTag>())
                uiText.text = (config.Level == 0 ? "BUY" : "LEVEL UP") + $"\nPrice: {config.NextLevelPrice}$";
        }

        private bool IsUpdateEventExist()
        {
            var isUpdateEventExist = false;
            foreach (var entityId in _updateViewEventsFilter)
            {
                _updateViewEventsFilter.GetEntity(entityId).Del<UpdateViewEvent>();
                isUpdateEventExist = true;
            }

            return isUpdateEventExist;
        }
    }
}