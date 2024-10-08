using Code.Progress.Provider;
using Entitas;

namespace Code.Meta.Features.Simulation
{
    public class UpdateSimulationTimeSystem : IExecuteSystem
    {
        private readonly MetaContext _meta;
        private readonly IProgressProvider _progress;
        private readonly IGroup<MetaEntity> _ticks;

        public UpdateSimulationTimeSystem(MetaContext game, IProgressProvider progress)
        {
            _meta = game;
            _progress = progress;
            _ticks = game.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.Tick));
        }

        public void Execute()
        {
            foreach (MetaEntity tick in _ticks)
            {
                _progress.ProgressData.LastSimulationTickTime = _progress.ProgressData.LastSimulationTickTime.AddSeconds(tick.Tick); 
            }
        }
    }
}
