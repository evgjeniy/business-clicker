using Ecs.Systems;
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
		
			AddInjections();
			AddSystems();
		
			_systems.Init();
		}
	
		private void AddInjections()
		{
			_systems
				.Inject(_businessNames)
				.Inject(_businessBusinessConfigs);
		}
	
		private void AddSystems()
		{
			_systems
				.Add(new ChangeFpsLimitSystem())
				.Add(new BusinessViewsSpawnSystem())
				.Add(new BusinessViewsSetupSystem())
				.Add(new DebugMessageSystem());
		}
	
		private void Update() => _systems.Run();

		private void OnDestroy()
		{
			_systems?.Destroy();
			_systems = null;
			_world?.Destroy();
			_world = null;
		}
	}
}