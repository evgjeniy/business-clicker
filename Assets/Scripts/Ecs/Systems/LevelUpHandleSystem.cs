﻿using Ecs.Components.Requests;
using Ecs.Extensions;
using Leopotam.Ecs;
using ScriptableObjects;

namespace Ecs.Systems
{
    public class LevelUpHandleSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessConfigDb _configDb = null;
        private readonly EcsFilter<OnLevelUpButtonClickRequest> _levelUpRequestsFilter = null;

        public void Run()
        {
            if (_levelUpRequestsFilter.IsEmpty()) return;
            
            foreach (var entityId in _levelUpRequestsFilter)
            {
                var businessIndex = _levelUpRequestsFilter.Get1(entityId).businessIndex;
                var businessConfig = _configDb.GetById(businessIndex);

                _world.SendMessage(new ReplenishBalanceRequest { value = -businessConfig.NextLevelPrice });
                businessConfig.Level += 1;

                _levelUpRequestsFilter.GetEntity(entityId).Del<OnLevelUpButtonClickRequest>();
            }
        }
    }
}