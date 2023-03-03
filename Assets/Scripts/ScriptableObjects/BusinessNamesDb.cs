using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Business Names DB", fileName = "BusinessNames")]
    public class BusinessNamesDb : ScriptableObject
    {
        [field: SerializeField] public List<BusinessName> BusinessNames { get; private set; }

        public BusinessName GetById(int id) => id < 0 || id >= BusinessNames.Count ? null : BusinessNames[id];

        public int Count => BusinessNames.Count;
    }

    [System.Serializable]
    public class BusinessName
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string FirstUpgradeName { get; private set; }
        [field: SerializeField] public string SecondUpgradeName { get; private set; }
    }
}