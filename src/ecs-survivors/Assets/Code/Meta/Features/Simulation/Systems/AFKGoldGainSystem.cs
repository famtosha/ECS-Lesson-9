using Entitas;

namespace Code.Meta.Features.Simulation
{
    public partial class AFKGoldGainSystem : IExecuteSystem
    {
        private readonly MetaContext _game;
        private readonly IGroup<MetaEntity> _ticks;
        private readonly IGroup<MetaEntity> _gainers;

        public AFKGoldGainSystem(MetaContext game)
        {
            _game = game;

            _ticks = game.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.Tick));

            _gainers = game.GetGroup(MetaMatcher
                .AllOf(
                MetaMatcher.GoldPerSercond,
                MetaMatcher.Gold,
                MetaMatcher.Storage));
        }

        public void Execute()
        {
            foreach (MetaEntity tick in _ticks)
            {
                foreach (MetaEntity gainer in _gainers)
                {
                    gainer.ReplaceGold(gainer.Gold + (gainer.GoldPerSercond * tick.Tick));
                }
            }
        }
    }
}
