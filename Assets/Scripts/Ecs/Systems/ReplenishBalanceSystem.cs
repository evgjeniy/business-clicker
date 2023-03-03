using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.UiComponents;
using Ecs.Extensions;
using Leopotam.Ecs;

namespace Ecs.Systems
{
    public class ReplenishBalanceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BalanceComponent> _balanceFilter = null;
        private readonly EcsFilter<ReplenishBalanceRequest> _replenishRequestsFilter = null;
        
        public void Run()
        {
            ref var balanceEntity = ref _balanceFilter.GetEntity(0);
            
            var isChanged = false;
            foreach (var entityId in _replenishRequestsFilter)
            {
                ref var entity = ref _replenishRequestsFilter.GetEntity(entityId);
                
                balanceEntity.Get<BalanceComponent>().MoneyAmount += entity.Get<ReplenishBalanceRequest>().value;
                entity.Del<ReplenishBalanceRequest>();
                
                isChanged = true;
            }

            if (isChanged) UpdateBalanceText(ref balanceEntity);
        }
        
        private void UpdateBalanceText(ref EcsEntity balanceEntity)
        {
            balanceEntity.Get<TextComponent>().uiText.text = 
                $"Balance: {balanceEntity.Get<BalanceComponent>().MoneyAmount}$";
            
            balanceEntity.SendMessage(new BalanceChangedEvent());
        }
    }
}