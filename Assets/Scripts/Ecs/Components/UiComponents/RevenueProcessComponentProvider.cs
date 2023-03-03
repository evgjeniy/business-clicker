using UnityEngine.UI;
using Voody.UniLeo;

namespace Ecs.Components.UiComponents
{
    public class RevenueProcessComponentProvider : MonoProvider<RevenueProcessComponent> {}

    [System.Serializable]
    public struct RevenueProcessComponent
    {
        public Slider uiSlider;
    }
}