using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.InitializeSystems
{
    public class InitializeBusinessNamesSystem : IEcsRunSystem
    {
        private readonly BusinessNamesDb _namesDb = null;
        private readonly EcsFilter<TextComponent, RootTransformComponent, InitializeEvent> _textFilter = null;

        public void Run()
        {
            foreach (var entityId in _textFilter)
            {
                ref var entity = ref _textFilter.GetEntity(entityId);
                ref var uiText = ref entity.Get<TextComponent>().uiText;
                
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessName = _namesDb.GetById(index);

                if (entity.Has<BusinessNameTag>())           uiText.text = businessName.name;
                else if (entity.Has<FirstUpgradeNameTag>())  uiText.text = businessName.firstUpgradeName;
                else if (entity.Has<SecondUpgradeNameTag>()) uiText.text = businessName.secondUpgradeName;
                else continue;
                
                entity.Del<InitializeEvent>();
            }
        }
    }
}