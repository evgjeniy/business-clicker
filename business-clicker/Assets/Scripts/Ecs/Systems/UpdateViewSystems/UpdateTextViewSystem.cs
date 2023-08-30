using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.UpdateViewSystems
{
    public abstract class UpdateTextViewSystem : IEcsRunSystem
    {
        public virtual void Run()
        {
            foreach (var entityId in ViewFilter)
            {
                ref var entity = ref ViewFilter.GetEntity(entityId);
                ref var uiText = ref entity.Get<TextComponent>().uiText;

                var businessIndex = entity.Get<RootTransformComponent>().rootTransform.GetSiblingIndex();
                var businessConfig = ConfigDb.GetById(businessIndex);
                
                uiText.text = GetUpdatedText(businessConfig);
                
                entity.Del<UpdateViewEvent>();
            }
        }
        
        protected abstract BusinessConfigDb ConfigDb { get; }

        protected abstract EcsFilter ViewFilter { get; }

        protected abstract string GetUpdatedText(BusinessConfig config);
    }
}