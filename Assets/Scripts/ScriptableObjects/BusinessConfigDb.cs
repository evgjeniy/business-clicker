using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Configuration DB", fileName = "Config")]
    public class BusinessConfigDb : ScriptableObject
    {
        [field: SerializeField]
        public List<BusinessConfig> BusinessConfigs { get; private set; }

        public BusinessConfig GetById(int id) => id < 0 || id >= BusinessConfigs.Count ? null : BusinessConfigs[id];

        public int Count => BusinessConfigs.Count;
    }
    
    [System.Serializable]
    public class BusinessConfig
    {
        [field: SerializeField, Min(0.0f)] public float RevenueDelay { get; private set; }
        [field: SerializeField, Min(0.0f)] public float BasePrice { get; private set; }
        [field: SerializeField, Min(0.0f)] public float BaseRevenue { get; private set; }
        [field: SerializeField] public UpgradeConfig FirstUpgrade { get; private set; }
        [field: SerializeField] public UpgradeConfig SecondUpgrade { get; private set; }
    }

    [System.Serializable]
    public class UpgradeConfig
    {
        [field: SerializeField, Min(0.0f)] public float Price { get; private set; }
        [field: SerializeField, Min(0.0f)] public float RevenueMultiplier { get; private set; }
    }
}