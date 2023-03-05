using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Save Data", fileName = "SaveDataConfig")]
    public class SaveDataConfig : ScriptableObject
    {
        [field: SerializeField] public float MoneyAmount { get; set; }
        [field: SerializeField] public BusinessNamesDb BusinessNamesDb { get; set; }
        [field: SerializeField] public BusinessConfigDb BusinessConfigDb { get; set; }

        private const string SaveDataKey = "SaveDataKey";

        public void TryLoad()
        {
            if (!PlayerPrefs.HasKey(SaveDataKey)) return;
            
            var json = PlayerPrefs.GetString(SaveDataKey);
            var saveData = JsonUtility.FromJson<SaveData>(json);

            MoneyAmount = saveData.moneyAmount;
            BusinessNamesDb.BusinessNames = saveData.businessNames.ToList();
            BusinessConfigDb.BusinessConfigs = saveData.businessConfigs.ToList();
        }

        public void Save()
        {
            var jsonSaveData = JsonUtility.ToJson(new SaveData
            {
                moneyAmount = MoneyAmount,
                businessNames = BusinessNamesDb.BusinessNames.ToArray(),
                businessConfigs = BusinessConfigDb.BusinessConfigs.ToArray()
            });
            
            PlayerPrefs.SetString(SaveDataKey, jsonSaveData);
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public float moneyAmount;
        public BusinessName[] businessNames;
        public BusinessConfig[] businessConfigs;
    }
}