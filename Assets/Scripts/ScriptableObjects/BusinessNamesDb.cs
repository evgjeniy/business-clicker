using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Business Names DB", fileName = "BusinessNames")]
    public class BusinessNamesDb : ScriptableObject
    {
        [field: SerializeField] public List<BusinessName> BusinessNames { get; set; }

        public BusinessName GetById(int id) => id < 0 || id >= BusinessNames.Count ? null : BusinessNames[id];

        public int Size => BusinessNames.Count;
    }

    [System.Serializable]
    public class BusinessName
    {
        public string name;
        public string firstUpgradeName;
        public string secondUpgradeName;
    }
}