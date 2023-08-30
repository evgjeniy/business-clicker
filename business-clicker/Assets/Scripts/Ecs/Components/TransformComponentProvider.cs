using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class TransformComponentProvider : MonoProvider<TransformComponent>
    {
        private void Awake() => value.Transform = transform;
    }

    public struct TransformComponent
    {
        public Transform Transform;
    }
}