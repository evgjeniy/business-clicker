using Ecs.Components;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class SetupBusinessNamesSystem : IEcsRunSystem
    {
        private readonly BusinessNamesDb _namesDb;
        private readonly EcsFilter<TextComponent, RootTransformComponent> _textFilter = null;

        public void Run()
        {
            foreach (var entityId in _textFilter)
            {
                ref var entity = ref _textFilter.GetEntity(entityId);
                ref var uiText = ref entity.Get<TextComponent>().uiText;
                
                var index = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessName = _namesDb.GetById(index);

                if (entity.Has<BusinessNameTag>())           uiText.text = businessName.Name;
                else if (entity.Has<FirstUpgradeNameTag>())  uiText.text = businessName.FirstUpgradeName;
                else if (entity.Has<SecondUpgradeNameTag>()) uiText.text = businessName.SecondUpgradeName;
                else continue;
                
                RemoveEntity(_textFilter.GetEntity(entityId));
            }
        }

        private void RemoveEntity(EcsEntity entity)
        {
            entity.Del<TextComponent>();
            entity.Del<RootTransformComponent>();
        }
    }
}