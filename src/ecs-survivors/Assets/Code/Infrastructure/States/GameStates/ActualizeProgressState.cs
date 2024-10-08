using Code.Common.Entity;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Infrastructure.Systems;
using Code.Meta.Features.Simulation;
using Code.Progress.Data;
using Code.Progress.Provider;
using System;

namespace Code.Infrastructure.States.GameStates
{
    public class ActualizeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly ISystemFactory _systems;
        private readonly ITimeService _time;
        private ActualizationFeature _actualizationFeature;
        private TimeSpan TwoDays = TimeSpan.FromDays(2);

        public ActualizeProgressState(IGameStateMachine stateMachine, IProgressProvider progressProvider, ISystemFactory systems, ITimeService time)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _systems = systems;
            _time = time;
        }

        public void Enter()
        {
            _actualizationFeature = _systems.Create<ActualizationFeature>();
            _progressProvider.ProgressData.LastSimulationTickTime = _time.UtcNow - TwoDays;
            ActualizeProgress(_progressProvider.ProgressData);
            _stateMachine.Enter<LoadingHomeScreenState>();
        }

        private void ActualizeProgress(ProgressData progressData)
        {
            _actualizationFeature.Initialize();

            CreateMetaEntity
                .Empty()
                .AddGoldGainBoost(1)
                .AddDuration(TimeSpan.FromDays(2).Seconds)
                ;

            DateTime until = GetDelta(progressData);

            while (progressData.LastSimulationTickTime < until)
            {
                var tick = CreateMetaEntity
                    .Empty()
                    .AddTick(1f)
                    ;

                _actualizationFeature.Execute();
                _actualizationFeature.Cleanup();

                tick.Destroy();
            }

            progressData.LastSimulationTickTime = _time.UtcNow;
        }

        private DateTime GetDelta(ProgressData progressData)
        {
            if ((_time.UtcNow - progressData.LastSimulationTickTime) < TwoDays)
            {
                return _time.UtcNow;
            }
            else
            {
                return progressData.LastSimulationTickTime + TwoDays;
            }
        }

        public void Exit()
        {
            _actualizationFeature.Cleanup();
            _actualizationFeature.TearDown();
            _actualizationFeature = null;
        }
    }
}
