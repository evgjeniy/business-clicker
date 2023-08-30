using Ecs.Components.Events;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.UpdateViewSystems
{
    public class UpdateBalanceViewSystem : IEcsRunSystem
    {
        private readonly SaveDataConfig _saveDataConfig = null;
        private readonly EcsFilter<TextComponent, UpdateViewEvent, BalanceViewTag> _viewFilter = null;

        public void Run()
        {
            foreach (var entityId in _viewFilter)
            {
                ref var entity = ref _viewFilter.GetEntity(entityId);
                ref var uiText = ref entity.Get<TextComponent>().uiText;
                
                uiText.text = $"Balance: {_saveDataConfig.MoneyAmount}$";
                
                entity.Del<UpdateViewEvent>();
            }
        }
    }
}