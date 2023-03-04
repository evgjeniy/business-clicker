using Ecs.Components;
using Ecs.Components.Events;
using Ecs.Components.Tags.Texts;
using Ecs.Components.UiComponents;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems.UpdateViewSystems
{
    public class UpdateSecondUpgradePriceViewSystem : UpdateTextViewSystem
    {
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<TextComponent, RootTransformComponent, UpdateViewEvent, SecondUpgradePriceTag> _viewFilter = null;

        protected override BusinessConfigDb ConfigDb => _configDb;
        
        protected override EcsFilter ViewFilter => _viewFilter;

        protected override string GetUpdatedText(BusinessConfig businessIndex) =>
            businessIndex.SecondUpgrade.IsPurchased ? "Purchased" : $"Price: {businessIndex.SecondUpgrade.Price}$";
    }
}