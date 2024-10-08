using Entitas;

namespace Code.Meta.Features.Simulation
{
    public partial class AFKGoldGainSystem
    {
        public class BoosterDurationSystem : IExecuteSystem
        {
            private readonly MetaContext _meta;
            private readonly IGroup<MetaEntity> _boosters;
            private readonly IGroup<MetaEntity> _ticks;

            public BoosterDurationSystem(MetaContext meta)
            {
                _meta = meta;
                _boosters = meta.GetGroup(MetaMatcher
                    .AllOf(
                    MetaMatcher.GoldGainBoost,
                    MetaMatcher.Duration));

                _ticks = meta.GetGroup(MetaMatcher
                    .AllOf(
                    MetaMatcher.Tick));
            }

            public void Execute()
            {
                foreach (MetaEntity booster in _boosters)
                {
                    foreach (MetaEntity tick in _ticks)
                    {
                        booster.ReplaceDuration(booster.Duration - tick.Tick);
                        if (booster.Duration <= 0)
                        {
                            booster.isDestructed = true;
                        }
                    }
                }
            }
        }
    }
}
