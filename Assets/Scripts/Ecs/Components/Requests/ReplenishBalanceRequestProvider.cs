using Voody.UniLeo;

namespace Ecs.Components.Requests
{
    public class ReplenishBalanceRequestProvider : MonoProvider<ReplenishBalanceRequest> {}
    
    [System.Serializable]
    public struct ReplenishBalanceRequest
    {
        public float value;
    }
}