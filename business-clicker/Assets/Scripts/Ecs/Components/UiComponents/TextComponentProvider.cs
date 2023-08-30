using UnityEngine.UI;
using Voody.UniLeo;

namespace Ecs.Components.UiComponents
{
    public class TextComponentProvider : MonoProvider<TextComponent> {}

    [System.Serializable]
    public struct TextComponent
    {
        public Text uiText;
    }
}