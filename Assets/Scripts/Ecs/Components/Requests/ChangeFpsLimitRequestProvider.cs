using Voody.UniLeo;

namespace Ecs.Components.Requests
{
    public class ChangeFpsLimitRequestProvider : MonoProvider<ChangeFpsLimitRequest> {}

    [System.Serializable]
    public struct ChangeFpsLimitRequest
    {
        public int newFpsLimit;
        public int newVSyncCount;
    }
}