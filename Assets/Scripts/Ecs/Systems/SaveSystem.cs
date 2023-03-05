using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Utilities;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Ecs.Systems
{
    public class SaveSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<SaveEvent, InitializeEvent> _saveEventsFilter = null;
        
        private const string ConfigDbSaveKey = "ConfigDatabaseSaveKey";
        private const string BalanceMoneyAmountSaveKey = "BalanceMoneyAmountSaveKey";
        
        public void Run()
        {
            foreach (var entityId in _saveEventsFilter)
            {
                ref var entity = ref _saveEventsFilter.GetEntity(entityId);

                entity.Get<SaveEvent>().OnSaveComplete = SaveData;
                entity.Del<InitializeEvent>();
            }
        }

        private void SaveData()
        {
            var jsonConfigDb = JsonUtility.ToJson(_configDb);
            PlayerPrefs.SetString(ConfigDbSaveKey, jsonConfigDb);
            PlayerPrefs.SetFloat(BalanceMoneyAmountSaveKey, _world.GetComponent<BalanceComponent>().MoneyAmount);
        }
    }
}