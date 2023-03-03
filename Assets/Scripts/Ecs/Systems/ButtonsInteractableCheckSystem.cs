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
        private readonly BusinessConfigDb _config = null;
        private readonly EcsFilter<BalanceComponent, BalanceChangedEvent> _balanceFilter = null;
        
        private readonly EcsFilter<ButtonComponent, RootTransformComponent, LevelUpButtonTag> _levelUpButtonsFilter = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent, FirstUpgradeButtonTag> _firstUpgradeButtonsFilter = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent, SecondUpgradeButtonTag> _secondUpgradeButtonsFilter = null;

        public void Run()
        {
            if (_balanceFilter.IsEmpty()) return;

            ref var balanceEntity = ref _balanceFilter.GetEntity(0);
            var moneyAmount = balanceEntity.Get<BalanceComponent>().MoneyAmount;
            
            CheckLevelUpButtons(moneyAmount);
            CheckFirstUpgradeButtons(moneyAmount);
            CheckSecondUpgradeButtons(moneyAmount);
            
            balanceEntity.Del<BalanceChangedEvent>();
        }

        private void CheckLevelUpButtons(float moneyAmount)
        {
            foreach (var entityId in _levelUpButtonsFilter)
            {
                var index = _levelUpButtonsFilter.Get2(entityId).rootTransform.GetSiblingIndex();
                var businessConfig = _config.GetById(index);
                var nextLevelPrice = (businessConfig.Level + 1) * businessConfig.BasePrice;

                _levelUpButtonsFilter.Get1(entityId).uiButton.interactable = moneyAmount >= nextLevelPrice;
            }
        }

        private void CheckFirstUpgradeButtons(float moneyAmount)
        {
            foreach (var entityId in _firstUpgradeButtonsFilter)
            {
                var index = _firstUpgradeButtonsFilter.Get2(entityId).rootTransform.GetSiblingIndex();
                var firstUpgradePrice = _config.GetById(index).FirstUpgrade.Price;

                _firstUpgradeButtonsFilter.Get1(entityId).uiButton.interactable = moneyAmount >= firstUpgradePrice;
            }
        }

        private void CheckSecondUpgradeButtons(float moneyAmount)
        {
            foreach (var entityId in _secondUpgradeButtonsFilter)
            {
                var index = _secondUpgradeButtonsFilter.Get2(entityId).rootTransform.GetSiblingIndex();
                var secondUpgradePrice = _config.GetById(index).SecondUpgrade.Price;

                _secondUpgradeButtonsFilter.Get1(entityId).uiButton.interactable = moneyAmount >= secondUpgradePrice;
            }
        }
    }
}