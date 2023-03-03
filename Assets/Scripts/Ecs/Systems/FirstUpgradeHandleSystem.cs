using Ecs.Components.Requests;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class FirstUpgradeHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<OnFirstUpgradeButtonClickEvent> _levelUpRequestsFilter = null;

        public void Run()
        {
            if (_levelUpRequestsFilter.IsEmpty()) return;
            
            foreach (var entityId in _levelUpRequestsFilter)
            {
                var businessIndex = _levelUpRequestsFilter.Get1(entityId).businessIndex;
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.FirstUpgrade.Price });
                businessConfig.FirstUpgrade.IsPurchased = true;

                _levelUpRequestsFilter.GetEntity(entityId).Del<OnFirstUpgradeButtonClickEvent>();
            }
        }
    }
}