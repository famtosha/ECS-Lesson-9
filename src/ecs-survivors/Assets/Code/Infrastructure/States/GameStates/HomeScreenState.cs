using Code.Gameplay;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.Systems;
using Code.Meta;

namespace Code.Infrastructure.States.GameStates
{
    public class HomeScreenState : IState, IUpdateable
    {
        private readonly ISystemFactory _systems;
        private readonly GameContext _context;
        private HomeScreenFeature _homeScreenFeature;

        public HomeScreenState(ISystemFactory systems, GameContext context)
        {
            _systems = systems;
            _context = context;
        }

        public void Enter()
        {
            _homeScreenFeature = _systems.Create<HomeScreenFeature>();
            _homeScreenFeature.Initialize();
        }

        public void Update()
        {
            _homeScreenFeature.Execute();
            _homeScreenFeature.Cleanup();
        }

        public void Exit()
        {
            _homeScreenFeature.DeactivateReactiveSystems();
            _homeScreenFeature.ClearReactiveSystems();

            _homeScreenFeature.Cleanup();
            _homeScreenFeature.TearDown();

            DestructEntities();

            _homeScreenFeature = null;
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