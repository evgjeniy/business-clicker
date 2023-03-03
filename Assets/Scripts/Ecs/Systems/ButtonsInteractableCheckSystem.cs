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
        private readonly EcsFilter<BalanceComponent, BalanceChangedEvent> _changedBalanceFilter = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent> _buttonsFilter = null;

        public void Run()
        {
            if (_changedBalanceFilter.IsEmpty()) return;

            ref var balanceEntity = ref _changedBalanceFilter.GetEntity(0);
            var moneyAmount = balanceEntity.Get<BalanceComponent>().MoneyAmount;
            
            foreach (var entityId in _buttonsFilter)
            {
                ref var entity = ref _buttonsFilter.GetEntity(entityId);
                
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = _configDb.GetById(index);

                SetInteractable(ref entity, businessConfig, moneyAmount);
            }
            
            balanceEntity.Del<BalanceChangedEvent>();
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
                entity.Get<ButtonComponent>().uiButton.interactable = moneyAmount >= firstUpgradePrice;
            }
            else if (entity.Has<SecondUpgradeButtonTag>())
            {
                var secondUpgradePrice = businessConfig.SecondUpgrade.Price;
                entity.Get<ButtonComponent>().uiButton.interactable = moneyAmount >= secondUpgradePrice;
            }
        }
    }
}