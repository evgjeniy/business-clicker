using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class PrefabProvider : MonoProvider<PrefabComponent> {}
    
    [System.Serializable]
    public struct PrefabComponent
    {
        public GameObject prefab;
    }
}