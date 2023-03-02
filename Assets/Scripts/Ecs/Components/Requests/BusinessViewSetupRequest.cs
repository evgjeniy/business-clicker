using ScriptableObjects;

namespace Ecs.Components.Requests
{
    public struct BusinessViewSetupRequest
    {
        public int SiblingIndex;
        public BusinessName BusinessName;
        public BusinessConfig BusinessConfig;
    }
}