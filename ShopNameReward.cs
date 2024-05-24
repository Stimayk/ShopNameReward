using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using Microsoft.Extensions.Logging;
using ShopAPI;

namespace ShopNameReward
{
    public class ShopNameReward : BasePlugin, IPluginConfig<ShopNameRewardConfig>
    {
        public override string ModuleName => "[SHOP] Name Reward";
        public override string ModuleDescription => "";
        public override string ModuleAuthor => "E!N";
        public override string ModuleVersion => "v1.0";

        private IShopApi? SHOP_API;

        public ShopNameRewardConfig Config { get; set; } = new();

        public void OnConfigParsed(ShopNameRewardConfig config)
        {
            Config = config;
        }

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            SHOP_API = IShopApi.Capability.Get();

            if (SHOP_API == null)
            {
                Logger.LogError("Shop API is not available.");
                return;
            }

            AddTimer(Config.Time, OnTimerElapsed, TimerFlags.REPEAT);
        }

        private void OnTimerElapsed()
        {
            foreach (var player in Utilities.GetPlayers().Where(p => p.IsValid && !p.IsBot))
            {
                if (player.PlayerName.Contains(Config.Advert))
                {
                    if (SHOP_API != null)
                    {
                        SHOP_API.SetClientCredits(player, SHOP_API.GetClientCredits(player) + Config.Credits);
                        player.PrintToChat(Localizer["HaveTag", Config.Credits, Config.Advert]);
                    }
                }
                else
                {
                    player.PrintToChat(Localizer["NeedTag", Config.Advert]);
                }
            }
        }
    }
}