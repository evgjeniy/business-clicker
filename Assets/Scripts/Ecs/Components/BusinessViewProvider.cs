using UnityEngine;
using UnityEngine.UI;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class BusinessViewProvider : MonoProvider<BusinessView> {}

    [System.Serializable]
    public struct BusinessView
    {
        [Header("Names")] 
        public Text name;
        public Text firstUpgradeName;
        public Text secondUpgradeName;

        [Header("Buttons")] 
        public Button levelUp;
        public Button firstUpgrade;
        public Button secondUpgrade;
        
        [Header("AnotherData")] 
        public Slider revenueDelayProcess;
        public Text levelNumber;
        public Text revenue;
    }
}