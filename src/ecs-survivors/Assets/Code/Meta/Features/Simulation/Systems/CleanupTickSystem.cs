using Entitas;
using System.Collections.Generic;

namespace Code.Meta.Features.Simulation
{
    public class CleanupTickSystem : ICleanupSystem
    {
        private readonly MetaContext _meta;
        private readonly IGroup<MetaEntity> _ticks;
        private readonly List<MetaEntity> _buffer = new List<MetaEntity>();

        public CleanupTickSystem(MetaContext meta)
        {
            _meta = meta;
            _ticks = _meta.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.Tick));
        }

        public void Cleanup()
        {
            foreach (MetaEntity tick in _ticks.GetEntities(_buffer))
            {
                tick.Destroy();
            }
        }
    }
}
