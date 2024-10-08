using Code.Common.Entity;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.Systems;

namespace Code.Meta.Features.Simulation
{
    public class EmitTickSystem : TimerExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly float _executeInterval;

        public EmitTickSystem(float executeInterval, ITimeService time) : base(executeInterval, time)
        {
            _executeInterval = executeInterval;
        }

        protected override void Execute()
        {
            CreateMetaEntity
                .Empty()
                .AddTick(_executeInterval)
                ;
        }
    }
}
