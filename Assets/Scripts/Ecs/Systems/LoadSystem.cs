using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Utilities;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class LoadSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<LoadEvent> _saveEventsFilter = null;
        
        private const string ConfigDbSaveKey = "ConfigDbSaveKey";
        private const string BalanceMoneyAmountSaveKey = "BalanceMoneyAmountSaveKey";

        public void Init()
        {
            foreach (var entityId in _saveEventsFilter)
            {
                _saveEventsFilter.GetEntity(entityId).Del<InitializeEvent>();
                TryLoadConfigDb();
            }
        }

        private void TryLoadConfigDb()
        {
            if (PlayerPrefs.HasKey(BalanceMoneyAmountSaveKey))
            {
                ref var balanceComponent = ref _world.GetComponent<BalanceComponent>();
                balanceComponent.MoneyAmount = PlayerPrefs.GetFloat(BalanceMoneyAmountSaveKey);
            }
            
            if (PlayerPrefs.HasKey(ConfigDbSaveKey)) 
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(ConfigDbSaveKey), _configDb);
        }
    }
}