using Ecs.Systems;
using Ecs.Systems.ButtonEventHandlers;
using Ecs.Systems.Debug;
using Ecs.Systems.InitializeSystems;
using Ecs.Systems.UpdateViewSystems;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;
using Voody.UniLeo;

namespace Ecs
{
	public sealed class EcsStartup : MonoBehaviour
	{
		[Header("Databases")]
		[SerializeField] private BusinessNamesDb _businessNames;
		[SerializeField] private BusinessConfigDb _businessBusinessConfigs;
		
		private EcsWorld _world;
		private EcsSystems _systems;
	
		private void Start()
		{
			_world = new EcsWorld();
			_systems = new EcsSystems(_world).ConvertScene();
			
			AddSystems();
			AddInjections();
			
			_systems.Init();
		}

		private void AddSystems()
		{
			_systems
				.Add(new ChangeFpsLimitSystem())
				.Add(new BusinessViewsSpawnSystem())
				
				.Add(new InitializeBusinessNamesSystem())
				.Add(new InitializeOnClickButtonEventsSystem())
				
				.Add(new RevenueDelayProcessSystem())
				.Add(new ReplenishBalanceSystem())
				
				.Add(new LevelUpHandleSystem())
				.Add(new FirstUpgradeHandleSystem())
				.Add(new SecondUpgradeHandleSystem())

				.Add(new ButtonsInteractableCheckSystem())
				
				.Add(new UpdateLevelNumberTextViewSystem())
				.Add(new UpdateRevenueTextViewSystem())
				.Add(new UpdateLevelUpPriceTextViewSystem())
				.Add(new UpdateFirstUpgradePriceViewSystem())
				.Add(new UpdateFirstUpgradeRevenueViewSystem())
				.Add(new UpdateSecondUpgradePriceViewSystem())
				.Add(new UpdateSecondUpgradeRevenueViewSystem())

				.Add(new DebugMessageSystem());
		}

		private void AddInjections()
		{
			_systems
				.Inject(_businessNames)
				.Inject(_businessBusinessConfigs);
		}

		private void Update() => _systems?.Run();

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems = null;
			_world?.Destroy();
			_world = null;
		}
	}
}