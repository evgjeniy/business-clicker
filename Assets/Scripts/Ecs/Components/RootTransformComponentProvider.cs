using UnityEngine;
using Voody.UniLeo;

namespace Ecs.Components
{
    public class RootTransformComponentProvider : MonoProvider<RootTransformComponent> {}

    [System.Serializable]
    public struct RootTransformComponent
    {
        public Transform rootTransform;
    }
}