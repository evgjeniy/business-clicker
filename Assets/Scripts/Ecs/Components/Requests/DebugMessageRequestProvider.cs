using Voody.UniLeo;

namespace Ecs.Components.Requests
{
    public enum MessageType { Log, Warning, Error }
    
    public class DebugMessageRequestProvider : MonoProvider<DebugMessageRequest> {}

    [System.Serializable]
    public struct DebugMessageRequest
    {
        public MessageType type; 
        public string message;
    }
}