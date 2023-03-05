using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Buttons;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class ButtonsInteractableCheckSystem : IEcsRunSystem
    {
        private readonly BusinessConfigDb _configDb = null;
        private readonly SaveDataConfig _saveDataConfig = null;
        
        private readonly EcsFilter<BalanceViewTag, UpdateViewEvent> _changedBalanceFilter = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent> _buttonsFilter = null;

        public void Run()
        {
            if (_changedBalanceFilter.IsEmpty()) return;
            
            foreach (var entityId in _buttonsFilter)
            {
                ref var entity = ref _buttonsFilter.GetEntity(entityId);
                
                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                SetInteractable(ref entity, _configDb.GetById(businessIndex));
            }
        }

        private void SetInteractable(ref EcsEntity entity, BusinessConfig businessConfig)
        {
            var moneyAmount = _saveDataConfig.MoneyAmount;
            
            if (entity.Has<LevelUpButtonTag>())
            {
                var nextLevelPrice = (businessConfig.level + 1) * businessConfig.basePrice;
                entity.Get<ButtonComponent>().uiButton.interactable = moneyAmount >= nextLevelPrice;
            }
            else if (entity.Has<FirstUpgradeButtonTag>())
            {
                var firstUpgradePrice = businessConfig.firstUpgrade.price;
                var isPurchased = businessConfig.firstUpgrade.isPurchased;
                entity.Get<ButtonComponent>().uiButton.interactable = !isPurchased && moneyAmount >= firstUpgradePrice;
            }
            else if (entity.Has<SecondUpgradeButtonTag>())
            {
                var secondUpgradePrice = businessConfig.secondUpgrade.price;
                var isPurchased = businessConfig.secondUpgrade.isPurchased;
                entity.Get<ButtonComponent>().uiButton.interactable = !isPurchased && moneyAmount >= secondUpgradePrice;
            }
        }
    }
}