using System;
using UltraSpeedrunHelper.Speedrun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UltraSpeedrunHelper.UltraSpeedrunHelper.UI
{
    public class Timer : MonoBehaviour
    {
        StatsManager stats = MonoSingleton<StatsManager>.Instance;
        float seconds;
        string timeString;

        private void OnGUI()
        {
            GameObject gameObject = GameObject.Find("Player");
            Rect rect = new Rect(new Vector2(20f, 20f), new Vector2(600f, 600f));
            Rect rect2 = new Rect(new Vector2(20f, 20f), new Vector2(100f, 20f));
            bool flag = gameObject != null;
            if (flag)
            {
                SpeedrunHelper.category = GUI.TextField(rect2, SpeedrunHelper.category, 25);

                rect.y += 20f;                
                
                GUI.Label(rect, timeString);
                

                rect.y += 20f;
                string text = "";
                text = string.Concat(new string[]
                {
                text
                });
                GUI.Label(rect, text);
                rect.y += 20f;
            }
        }

        private void Update()
        {


            SceneManager.sceneLoaded += OnSceneLoaded;

            Scene scene = SceneManager.GetActiveScene();
            bool reset = false;

            if (scene.name.Contains("Level"))
            {
                reset = false;
            }
            else reset = true;

            if(reset == true)
            {
                if (Keyboard.current.lKey.wasPressedThisFrame)
                {
                    SpeedrunHelper.currentTime = 0;
                }
            }

            if (stats != null)
            {
                if (SpeedrunHelper.addLevelTime == true)
                {
                    seconds = stats.seconds + SpeedrunHelper.currentTime;

                    TimeSpan time = TimeSpan.FromSeconds(seconds);
                    timeString = time.ToString(@"h\:mm\:ss\.fff");
                }
                if (SpeedrunHelper.addLevelTime == false)
                {
                    seconds = SpeedrunHelper.currentTime;

                    TimeSpan time = TimeSpan.FromSeconds(seconds);
                    timeString = time.ToString(@"h\:mm\:ss\.fff");
                }
            }
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SpeedrunHelper.addLevelTime = true;
            if(scene.name == "Intro")
            {
                return;
            }
            stats = MonoSingleton<StatsManager>.Instance;

        }

        public static Sprite LoadSprite(Texture2D texture, Vector4 border, float pixelsPerUnit)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit, 0, SpriteMeshType.Tight, border);
        }
    }
}
