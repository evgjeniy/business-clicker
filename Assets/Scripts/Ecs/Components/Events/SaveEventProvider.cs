using Voody.UniLeo;

namespace Ecs.Components.Events
{
    public class SaveEventProvider : MonoProvider<SaveEvent> {}
    
    public struct SaveEvent
    {
        public System.Action OnSaveComplete;
    }
}