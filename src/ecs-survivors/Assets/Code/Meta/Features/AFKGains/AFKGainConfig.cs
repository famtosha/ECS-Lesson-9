using UnityEngine;

namespace Code.Meta.Features.AFKGains
{
    [CreateAssetMenu(fileName = nameof(AFKGainConfig), menuName = "Configs/AFKGainConfig")]
    public class AFKGainConfig : ScriptableObject
    {
        [field: SerializeField] public float GoldPerSecond { get; private set; }

        [field: SerializeField] public float GemPerSecond { get; private set; }
        [field: SerializeField] public float GemGainChance { get; private set; }
    }
}
