using Code.Common.Destruct;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation;

namespace Code.Meta
{
    public class HomeScreenFeature : Feature
    {
        public HomeScreenFeature(ISystemFactory systems)
        {
            Add(systems.Create<EmitTickSystem>(1f));

            Add(systems.Create<SimulationFeature>());
            Add(systems.Create<ActualizationFeature>());

            Add(systems.Create<CleanupTickSystem>());

            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}
