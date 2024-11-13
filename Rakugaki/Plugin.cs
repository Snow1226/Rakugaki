using IPA;
using IPA.Config;
using IPA.Config.Stores;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;
using Rakugaki.Configuration;
using Rakugaki.UI;

namespace Rakugaki
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin instance { get; private set; }
        private Harmony _harmony;
        internal static string Name => "Rakugaki";
        public RakugakiController rakugakiController;
        public float penSize = 0.01f;
        public Color penColor = Color.white;
        public float drawDelay = 0.1f;
        [Init]
        public void Init(Config conf, IPALogger logger)
        {
            instance = this;
            PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Logger.log = logger;
            Logger.log.Debug("Logger initialized.");
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Logger.log.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            _harmony = new Harmony("com.Snow1226.Rakugaki");
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Logger.log.Debug("OnApplicationStart");
            rakugakiController=new GameObject("RakugakiController").AddComponent<RakugakiController>();

            BeatSaberMarkupLanguage.Util.MainMenuAwaiter.MainMenuInitializing += MenuInit;
        }

        public void MenuInit()
        {
            UIManager.Init();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");
            _harmony.UnpatchSelf();
        }

    }
}
