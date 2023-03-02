namespace Ecs.Components.Requests
{
    public enum MessageType { Log, Warning, Error }
    
    public struct DebugMessageRequest
    {
        public MessageType Type;
        public string Message;
    }
}