using Ecs.Components.Requests;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems
{
    public class ChangeFpsLimitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ChangeFpsLimitRequest> _fpsLimitFilter = null;

        public void Init()
        {
            foreach (var entityId in _fpsLimitFilter)
            {
                ref var fpsLimitRequest = ref _fpsLimitFilter.Get1(entityId);

                Application.targetFrameRate = fpsLimitRequest.newFpsLimit;
                QualitySettings.vSyncCount = fpsLimitRequest.newVSyncCount;
                
                _fpsLimitFilter.GetEntity(entityId).Del<ChangeFpsLimitRequest>();
            }
        }
    }
}