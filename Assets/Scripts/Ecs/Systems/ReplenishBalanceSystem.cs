using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Utilities;
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

            if (isChanged) balanceEntity.SendMessage(new UpdateViewEvent());
        }
    }
}