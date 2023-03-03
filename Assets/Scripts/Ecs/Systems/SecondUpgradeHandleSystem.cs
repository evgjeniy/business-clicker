using Ecs.Components.Requests;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class SecondUpgradeHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<OnSecondUpgradeButtonClickEvent> _levelUpRequestsFilter = null;

        public void Run()
        {
            if (_levelUpRequestsFilter.IsEmpty()) return;
            
            foreach (var entityId in _levelUpRequestsFilter)
            {
                var businessIndex = _levelUpRequestsFilter.Get1(entityId).businessIndex;
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.SecondUpgrade.Price });
                businessConfig.SecondUpgrade.IsPurchased = true;

                Debug.Log($"Minus {businessConfig.SecondUpgrade.Price}");
                
                _levelUpRequestsFilter.GetEntity(entityId).Del<OnSecondUpgradeButtonClickEvent>();
            }
        }
    }
}