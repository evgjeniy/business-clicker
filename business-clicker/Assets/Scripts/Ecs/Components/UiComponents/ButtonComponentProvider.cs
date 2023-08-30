using UnityEngine.UI;
using Voody.UniLeo;

namespace Ecs.Components.UiComponents
{
    public class ButtonComponentProvider : MonoProvider<ButtonComponent> {}

    [System.Serializable]
    public struct ButtonComponent
    {
        public Button uiButton;
    }
}