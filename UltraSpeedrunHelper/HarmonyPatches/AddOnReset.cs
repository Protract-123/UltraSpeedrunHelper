using HarmonyLib;

using UltraSpeedrunHelper.Speedrun;

namespace UltraSpeedrunHelper.UltraSpeedrunHelper.HarmonyPatches
{
    [HarmonyPatch(typeof(OptionsManager), "RestartMission")]
    public static class AddOnReset
    {
        public static void Prefix()
        {
            StatsManager stats = MonoSingleton<StatsManager>.Instance;
            SpeedrunHelper.currentTime += stats.seconds;
            SpeedrunHelper.addLevelTime = false;
        }
    }
}
