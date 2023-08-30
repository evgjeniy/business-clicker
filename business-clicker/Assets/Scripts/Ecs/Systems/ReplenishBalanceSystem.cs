using Ecs.Components.Events;
using Ecs.Components.Requests;
using Ecs.Components.Tags.Texts;
using Ecs.Utilities;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class ReplenishBalanceSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SaveDataConfig _saveDataConfig = null;
        private readonly EcsFilter<ReplenishBalanceRequest> _replenishRequestsFilter = null;
        
        public void Run()
        {
            var isChanged = false;
            foreach (var entityId in _replenishRequestsFilter)
            {
                ref var entity = ref _replenishRequestsFilter.GetEntity(entityId);
                
                _saveDataConfig.MoneyAmount += entity.Get<ReplenishBalanceRequest>().value;
                entity.Del<ReplenishBalanceRequest>();
                
                isChanged = true;
            }

            if (isChanged) _world.AddComponentByTag<BalanceViewTag, UpdateViewEvent>();
        }
    }
}