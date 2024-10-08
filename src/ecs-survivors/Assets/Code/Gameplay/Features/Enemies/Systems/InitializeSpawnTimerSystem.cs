using Code.Common.Entity;
using Code.Gameplay.Common;
using Entitas;

namespace Code.Gameplay.Features.Enemies.Systems
{
  public class InitializeSpawnTimerSystem : IInitializeSystem
  {
    public void Initialize()
    {
      CreateGameEntity.Empty()
        .AddSpawnTimer(GameplayConstants.EnemySpawnTimer);
    }
  }
}