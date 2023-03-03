using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Configuration DB", fileName = "Config")]
    public class BusinessConfigDb : ScriptableObject
    {
        [field: SerializeField] public List<BusinessConfig> BusinessConfigs { get; private set; }

        public BusinessConfig GetById(int id) => id < 0 || id >= BusinessConfigs.Count ? null : BusinessConfigs[id];

        public int Size => BusinessConfigs.Count;
    }
    
    
    [System.Serializable]
    public class BusinessConfig
    {
        [field: SerializeField] public int Level { get; set; }
        
        [field: Header("Business Balance Settings")]
        [field: SerializeField, Min(0.0f)] public float RevenueDelay { get; private set; }
        [field: SerializeField, Min(0.0f)] public float BasePrice { get; private set; }
        [field: SerializeField, Min(0.0f)] public float BaseRevenue { get; private set; }
        [field: SerializeField] public UpgradeConfig FirstUpgrade { get; private set; }
        [field: SerializeField] public UpgradeConfig SecondUpgrade { get; private set; }

        public float NextLevelPrice => (Level + 1) * BasePrice;

        public float GetCurrentRevenue()
        {
            var firstMultiplier = FirstUpgrade.IsPurchased ? FirstUpgrade.RevenueMultiplier : 0;
            var secondMultiplier = SecondUpgrade.IsPurchased ? SecondUpgrade.RevenueMultiplier : 0;

            return Level * BaseRevenue * (1 + firstMultiplier + secondMultiplier);
        }
    }

    
    [System.Serializable]
    public class UpgradeConfig
    {
        [field: SerializeField] public bool IsPurchased { get; set; }
        
        [field: Header("Upgrade Balance Settings")]
        [field: SerializeField, Min(0.0f)] public float Price { get; private set; }
        [field: SerializeField, Min(0.0f)] public float RevenueMultiplier { get; private set; }
    }
}