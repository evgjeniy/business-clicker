using UnityEngine.UI;
using Voody.UniLeo;

namespace Ecs.Components.UiComponents
{
    public class SliderComponentProvider : MonoProvider<SliderComponent> {}

    [System.Serializable]
    public struct SliderComponent
    {
        public Slider uiSlider;
    }
}