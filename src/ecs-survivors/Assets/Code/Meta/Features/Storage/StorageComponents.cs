using Entitas;

namespace Code.Meta.Features.Storage
{
    [Meta] public class Storage : IComponent { }
    [Meta] public class Gold : IComponent { public float Value; }
    [Meta] public class GoldPerSecond : IComponent { public float Value; }

    [Meta] public class Gems : IComponent { public float Value; }
    [Meta] public class GemsPerSecond : IComponent { public float Value; }

    [Meta] public class GainChance : IComponent { public float Value; }
}
