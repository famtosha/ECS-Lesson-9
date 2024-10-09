using Code.Gameplay.Common.Random;
using Entitas;

namespace Code.Meta.Features.Simulation
{
    public class AFKGemGainSystem : IExecuteSystem
    {
        private readonly MetaContext _game;
        private readonly IRandomService _random;
        private readonly IGroup<MetaEntity> _ticks;
        private readonly IGroup<MetaEntity> _gainers;

        public AFKGemGainSystem(MetaContext game, IRandomService random)
        {
            _game = game;
            _random = random;
            _ticks = game.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.Tick));

            _gainers = game.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.GemsPerSecond,
                MetaMatcher.Gems,
                MetaMatcher.GainChance,
                MetaMatcher.Storage));
        }

        public void Execute()
        {
            foreach (MetaEntity tick in _ticks)
            {
                foreach (MetaEntity gainer in _gainers)
                {
                    if (gainer.GainChance > _random.Range(0f, 1f))
                    {
                        gainer.ReplaceGems(gainer.Gems + (gainer.GemsPerSecond * tick.Tick));
                    }
                }
            }
        }
    }
}
