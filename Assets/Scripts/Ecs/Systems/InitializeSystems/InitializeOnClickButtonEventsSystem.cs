using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Buttons;
using Ecs.Components.UiComponents;
using Ecs.Extensions;
using Leopotam.Ecs;

namespace Ecs.Systems.InitializeSystems
{
    public class InitializeOnClickButtonEventsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<ButtonComponent, RootTransformComponent, InitializeEvent> _buttonsFilter = null;

        public void Run()
        {
            foreach (var entityId in _buttonsFilter)
            {
                ref var entity = ref _buttonsFilter.GetEntity(entityId);
                ref var button = ref entity.Get<ButtonComponent>().uiButton;
            
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex(); 

                if (entity.Has<LevelUpButtonTag>())
                    button.onClick.AddListener(() => SendButtonClickEvent<LevelUpButtonTag>(index));
                else if (entity.Has<FirstUpgradeButtonTag>())
                    button.onClick.AddListener(() => SendButtonClickEvent<FirstUpgradeButtonTag>(index));
                else if (entity.Has<SecondUpgradeButtonTag>())
                    button.onClick.AddListener(() => SendButtonClickEvent<SecondUpgradeButtonTag>(index));
                else continue;

                entity.Del<InitializeEvent>();
            }
        }

        private void SendButtonClickEvent<T>(int index) where T : struct => 
            _world.AddComponentByTag<T, OnButtonClickEvent>(index);
    }
}