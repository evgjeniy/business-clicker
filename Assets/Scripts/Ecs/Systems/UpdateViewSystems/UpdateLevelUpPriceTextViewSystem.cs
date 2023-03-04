using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.UpdateViewSystems
{
    public class UpdateLevelUpPriceTextViewSystem : UpdateTextViewSystem
    {
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<TextComponent, RootTransformComponent, UpdateViewEvent, LevelUpPriceTextTag> _viewFilter = null;

        protected override BusinessConfigDb ConfigDb => _configDb;
        
        protected override EcsFilter ViewFilter => _viewFilter;

        protected override string GetUpdatedText(BusinessConfig businessIndex) =>
            (businessIndex.Level == 0 ? "BUY" : "LEVEL UP") + $"\nPrice: {businessIndex.NextLevelPrice}$";
    }
}