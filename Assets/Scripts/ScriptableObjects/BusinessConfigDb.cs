using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Configuration DB", fileName = "Config")]
    public class BusinessConfigDb : ScriptableObject
    {
        [field: SerializeField] public List<BusinessConfig> BusinessConfigs { get; set; }

        public BusinessConfig GetById(int id) => id < 0 || id >= BusinessConfigs.Count ? null : BusinessConfigs[id];

        public int Size => BusinessConfigs.Count;

        [ContextMenu("Reset Data to Default Values")]
        private void ResetDataToStartValues()
        {
            foreach (var businessConfig in BusinessConfigs)
            {
                businessConfig.level = 0;
                businessConfig.currentProcess = 0.0f;
                businessConfig.firstUpgrade.isPurchased = false;
                businessConfig.secondUpgrade.isPurchased = false;
            }
            
            if (BusinessConfigs.Count > 0) BusinessConfigs[0].level = 1;
        }
    }
    
    [System.Serializable]
    public class BusinessConfig
    {
        [Min(0)] public int level;
        [Min(0.0f)] public float currentProcess;
        
        [Header("Business Balance Settings")]
        [Min(0.0f)] public float revenueDelay;
        [Min(0.0f)] public float basePrice;
        [Min(0.0f)] public float baseRevenue;
        
        [Header("Upgrades")]
        public UpgradeConfig firstUpgrade; 
        public UpgradeConfig secondUpgrade;

        public float NextLevelPrice => (level + 1) * basePrice;

        public float GetCurrentRevenue()
        {
            var firstMultiplier = firstUpgrade.isPurchased ? firstUpgrade.revenueMultiplier : 0;
            var secondMultiplier = secondUpgrade.isPurchased ? secondUpgrade.revenueMultiplier : 0;

            return level * baseRevenue * (1 + firstMultiplier + secondMultiplier);
        }
    }

    
    [System.Serializable]
    public class UpgradeConfig
    {
        [SerializeField] public bool isPurchased;
        
        [Header("Upgrade Balance Settings")]
        [Min(0.0f)] public float price;
        [Min(0.0f)] public float revenueMultiplier;
    }
}