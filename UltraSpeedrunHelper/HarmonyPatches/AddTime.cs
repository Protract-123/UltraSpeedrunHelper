using HarmonyLib;
using UltraSpeedrunHelper.Speedrun;
using GameConsole;

namespace UltraSpeedrunHelper.UltraSpeedrunHelper.HarmonyPatches
{
    [HarmonyPatch(typeof(FinalRank), "LoadNextLevel")]
    public static class AddTime
    {
        public static void Postfix()
        {
            SpeedrunHelper.addLevelTime = false;
            StatsManager stats = MonoSingleton<StatsManager>.Instance;
            Console.print(SpeedrunHelper.currentTime);
            Console.print(stats.seconds);

            
                float time = stats.seconds;
                SpeedrunHelper.currentTime += time;
            
        }
    }
}
