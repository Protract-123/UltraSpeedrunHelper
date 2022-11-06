using System;
using System.Collections;
using UltraSpeedrunHelper.Speedrun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Console = GameConsole.Console;
using Object = UnityEngine.Object;

namespace UltraSpeedrunHelper.UltraSpeedrunHelper.UI
{
    public class Timer : MonoBehaviour
    {
        StatsManager stats = MonoSingleton<StatsManager>.Instance;
        float seconds;


        Text text;
        private void Start()
        {
            
            bool usrh = false;
            bool gif = false;

            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            GameObject hud = camera.GetComponentInChildren<HudController>().gameObject;
            
            GameObject canvasGO = new GameObject("SHCanvas");
            canvasGO.AddComponent<Canvas>();
            
            Canvas canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            canvasGO.AddComponent<HUDPos>();

            canvasGO.transform.parent = hud.transform;



            AssetBundle bundle = null;
            AssetBundle gifBundle = null;

            IEnumerable assetBundles = AssetBundle.GetAllLoadedAssetBundles();
            foreach (AssetBundle assetBundle in assetBundles) {
                Console.print(assetBundle.name);
                if(assetBundle.name == "ursh")
                {
                    usrh = true;
                }
                if(assetBundle.name == "gif")
                {
                    gif = true; 
                }

            }
            if (!usrh) {
                bundle = AssetBundle.LoadFromMemory(Properties.Resources.usrh);
            }
            if (!gif)
            {
                gifBundle = AssetBundle.LoadFromMemory(Properties.Resources.gif);
            }


            if (bundle != null)
            {



                Object[] assets = bundle.LoadAllAssets();
                Console.print(assets[0].name);
                Console.print(assets[1].name);

                if (canvas != null)
                {

                    GameObject timer = new GameObject("Timer");
                    timer.AddComponent<HudOpenEffect>();
                    RectTransform timerSize = timer.GetComponent<RectTransform>();
                    timer.transform.parent = canvasGO.transform;

                    if (timerSize == null)
                    {
                        timerSize = timer.AddComponent<RectTransform>();
                    }
                    timerSize.sizeDelta = new Vector2(100, 50);

                    Image image = timer.AddComponent<Image>();

                    Texture2D asset1 = bundle.LoadAsset<Texture2D>(assets[0].name);
                    Texture2D asset2 = bundle.LoadAsset<Texture2D>(assets[1].name);
                    Console.print(asset2);

                    Sprite header = LoadSprite(asset2, Vector4.zero, 100);
                    image.sprite = header;

                    GameObject stopwatch = new GameObject("Stopwatch");
                    DontDestroyOnLoad(stopwatch);
                    stopwatch.transform.parent = timer.transform;

                    GameObject textGO = new GameObject("TimerText");
                    DontDestroyOnLoad(textGO);
                    text = textGO.AddComponent<Text>();
                    textGO.transform.parent = timer.transform;
                    


                }

            } 
            else Console.print("Bundle Not Loaded");

        }
        private void Update()
        {


            SceneManager.sceneLoaded += OnSceneLoaded;

            if (stats != null)
            {
                if (SpeedrunHelper.addLevelTime == true)
                {
                    seconds = stats.seconds + SpeedrunHelper.currentTime;

                    TimeSpan time = TimeSpan.FromSeconds(seconds);
                    string timeString = time.ToString(@"h\:mm\:ss\.fff");
                    Console.print(timeString);
                }
                if (SpeedrunHelper.addLevelTime == false)
                {
                    seconds = SpeedrunHelper.currentTime;

                    TimeSpan time = TimeSpan.FromSeconds(seconds);
                    string timeString = time.ToString(@"h\:mm\:ss\.fff");
                    Console.print(timeString);
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
