using Code.Gameplay.StaticData;
using Entitas;

namespace Code.Meta.Features.Simulation
{
    public partial class AFKGoldGainSystem
    {
        public class CalculateGoldGainSystem : IExecuteSystem
        {
            private readonly MetaContext _game;
            private readonly IStaticDataService _configs;
            private readonly IGroup<MetaEntity> _boosters;
            private readonly IGroup<MetaEntity> _storages;

            public CalculateGoldGainSystem(MetaContext game, IStaticDataService configs)
            {
                _game = game;
                _configs = configs;
                _boosters = game.GetGroup(MetaMatcher
                    .AllOf(
                    MetaMatcher.GoldGainBoost));

                _storages = game.GetGroup(MetaMatcher
                    .AllOf(
                    MetaMatcher.Storage,
                    MetaMatcher.GoldPerSercond));
            }

            public void Execute()
            {
                float gainMultplier = 1f;
                foreach (MetaEntity storage in _storages)
                {
                    foreach (MetaEntity booster in _boosters)
                    {
                        gainMultplier += booster.GoldGainBoost;
                        storage.ReplaceGoldPerSercond(_configs.AFKGain.GoldPerSecond * gainMultplier);
                    }
                }
            }
        }
    }
}
