using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;

namespace Ecs.Systems.UpdateViewSystems
{
    public class UpdateBalanceViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TextComponent, UpdateViewEvent, BalanceComponent> _viewFilter = null;

        public void Run()
        {
            foreach (var entityId in _viewFilter)
            {
                ref var entity = ref _viewFilter.GetEntity(entityId);
                ref var uiText = ref entity.Get<TextComponent>().uiText;
                
                uiText.text = $"Balance: {entity.Get<BalanceComponent>().MoneyAmount}$";
                
                entity.Del<UpdateViewEvent>();
            }
        }
    }
}