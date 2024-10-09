using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.Common.Time;
using Code.Gameplay.StaticData;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
{
    public class InitializeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;
        private readonly IStaticDataService _configs;
        private readonly ITimeService _time;

        public InitializeProgressState(IGameStateMachine stateMachine, IProgressProvider progressProvider, IStaticDataService configs, ITimeService time)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
            _configs = configs;
            _time = time;
        }

        public void Enter()
        {
            InitializeProgress();

            _stateMachine.Enter<ActualizeProgressState>();
        }

        private void InitializeProgress()
        {
            CreateNewProgress();
        }

        private void CreateNewProgress()
        {
            _progressProvider.SetProgressData(new ProgressData() { LastSimulationTickTime = _time.UtcNow });

            CreateMetaEntity
                .Empty()
                .With(x => x.isStorage = true)
                .AddGold(0)
                .AddGoldPerSecond(_configs.AFKGain.GoldPerSecond)
                ;

            CreateMetaEntity
                .Empty()
                .With(x => x.isStorage = true)
                .AddGems(0)
                .AddGemsPerSecond(_configs.AFKGain.GemPerSecond)
                .AddGainChance(_configs.AFKGain.GemGainChance)
                ;
        }

        public void Exit()
        {
        }
    }
}