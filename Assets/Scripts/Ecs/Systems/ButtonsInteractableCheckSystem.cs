using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Buttons;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class ButtonsInteractableCheckSystem : IEcsRunSystem
    {
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<BalanceComponent, UpdateViewEvent> _changedBalanceFilter = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent> _buttonsFilter = null;

        public void Run()
        {
            if (_changedBalanceFilter.IsEmpty()) return;

            var moneyAmount = _changedBalanceFilter.GetEntity(0).Get<BalanceComponent>().MoneyAmount;
            
            foreach (var entityId in _buttonsFilter)
            {
                ref var entity = ref _buttonsFilter.GetEntity(entityId);
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(businessIndex);

                SetInteractable(ref entity, businessConfig, moneyAmount);
            }
        }

        private void SetInteractable(ref EcsEntity entity, BusinessConfig businessConfig, float moneyAmount)
        {
            if (entity.Has<LevelUpButtonTag>())
            {
                var nextLevelPrice = (businessConfig.Level + 1) * businessConfig.BasePrice;
                entity.Get<ButtonComponent>().uiButton.interactable = moneyAmount >= nextLevelPrice;
            }
            else if (entity.Has<FirstUpgradeButtonTag>())
            {
                var firstUpgradePrice = businessConfig.FirstUpgrade.Price;
                var isPurchased = businessConfig.FirstUpgrade.IsPurchased;
                entity.Get<ButtonComponent>().uiButton.interactable = !isPurchased && moneyAmount >= firstUpgradePrice;
            }
            else if (entity.Has<SecondUpgradeButtonTag>())
            {
                var secondUpgradePrice = businessConfig.SecondUpgrade.Price;
                var isPurchased = businessConfig.SecondUpgrade.IsPurchased;
                entity.Get<ButtonComponent>().uiButton.interactable = !isPurchased && moneyAmount >= secondUpgradePrice;
            }
        }
    }
}