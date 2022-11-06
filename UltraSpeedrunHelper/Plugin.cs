using BepInEx;
using UnityEngine.SceneManagement;
using UltraSpeedrunHelper.UltraSpeedrunHelper.UI;
using HarmonyLib;

namespace UltraSpeedrunHelper
{
    [BepInPlugin("protract.uk.speedrunhelper", "SpeedrunHelper", "1.0.0")]
    [BepInProcess("ULTRAKILL.exe")]

    public class Plugin : BaseUnityPlugin
    {
        private Timer timer;
        private void Start()
        {
            var harmony = new Harmony("Protract.UK.SeedrunHelper");
            harmony.PatchAll();
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "Intro")
            {
                if(MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<Timer>(out _) == false)
                {
                    timer = MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Timer>();
                }
            }
        }
        private void Update()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
    }
}
    