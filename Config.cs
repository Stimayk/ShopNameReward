using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace ShopNameReward
{
    public class ShopNameRewardConfig : BasePluginConfig
    {
        [JsonPropertyName("Advert")]
        public string Advert { get; set; } = "csdevs.net";

        [JsonPropertyName("Credits")]
        public int Credits { get; set; } = 1;

        [JsonPropertyName("Time")]
        public float Time { get; set; } = 60.0f;
    }
}
