using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class TransformProvider : MonoProvider<TransformComponent>
    {
        private void Awake() => value.transform = transform;
    }

    [System.Serializable]
    public struct TransformComponent
    {
        public Transform transform;
    }
}