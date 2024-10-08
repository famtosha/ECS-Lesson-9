using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Infrastructure.Systems
{
    public abstract class TimerExecuteSystem : IExecuteSystem
    {
        private readonly float _executeIntervalSeconds;
        private readonly ITimeService _time;
        private float _timeExecute;

        protected TimerExecuteSystem(float executeInterval, ITimeService time)
        {
            _executeIntervalSeconds = executeInterval;
            _time = time;
        }

        protected abstract void Execute();

        void IExecuteSystem.Execute()
        {
            _timeExecute -= _time.DeltaTime;
            if (_timeExecute <= 0)
            {
                _timeExecute = _executeIntervalSeconds;
                Execute();
            }
        }
    }
}
