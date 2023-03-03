using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class PrefabComponentProvider : MonoProvider<PrefabComponent> {}
    
    [System.Serializable]
    public struct PrefabComponent
    {
        public GameObject prefab;
    }
}