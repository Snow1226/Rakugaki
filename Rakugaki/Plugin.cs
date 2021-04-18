using IPA;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;

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
        public void Init(IPALogger logger)
        {
            instance = this;
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
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");

        }

    }
}
