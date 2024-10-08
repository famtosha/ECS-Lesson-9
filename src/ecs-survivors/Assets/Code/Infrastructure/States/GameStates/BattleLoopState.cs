using Code.Gameplay;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using System;

namespace Code.Infrastructure.States.GameStates
{
    public class BattleLoopState : IState, IUpdateable
    {
        private readonly ISystemFactory _systems;
        private readonly GameContext _context;
        private BattleFeature _battleFeature;

        public BattleLoopState(ISystemFactory systems, GameContext context)
        {
            _systems = systems;
            _context = context;
        }

        public void Enter()
        {
            _battleFeature = _systems.Create<BattleFeature>();
            _battleFeature.Initialize();
        }

        public void Update()
        {
            _battleFeature.Execute();
            _battleFeature.Cleanup();
        }

        public void Exit()
        {
            _battleFeature.DeactivateReactiveSystems();
            _battleFeature.ClearReactiveSystems();

            _battleFeature.Cleanup();
            _battleFeature.TearDown();

            DestructEntities();

            _battleFeature = null;
        }

        private void DestructEntities()
        {
            foreach (GameEntity entity in _context.GetEntities())
            {
                entity.isDestructed = true; 
            }
        }
    }
}