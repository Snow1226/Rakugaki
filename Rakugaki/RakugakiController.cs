using UnityEngine;
using Rakugaki.HarmonyPatches;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using Rakugaki.UI;

namespace Rakugaki
{
    public class RakugakiController : MonoBehaviour
    {
        public static RakugakiController instance { get; private set; }
        internal static RakugakiRender _controller;
        public int drawCount = 0;
        private ModMainFlowCoordinator mainFlowCoordinator;

        private void Awake()
        {
            if (instance != null)
            {
                Logger.log?.Warn($"Instance of {this.GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            instance = this;
            Logger.log?.Debug($"{name}: Awake()");

            MenuButton menuButton = new MenuButton(
                "Rakugaki", "scribble mod", ShowModFlowCoordinator, true);
            MenuButtons.instance.RegisterButton(menuButton);
        }
        public void ShowModFlowCoordinator()
        {
            if (this.mainFlowCoordinator == null)
                this.mainFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<ModMainFlowCoordinator>();
            if (mainFlowCoordinator.IsBusy) return;

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(mainFlowCoordinator);
        }
        private void OnDestroy()
        {
            Logger.log?.Debug($"{name}: OnDestroy()");
            instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.

        }

        public void UndoDraw()
        {
            foreach(Transform child in this.transform)
            {
                if(child.gameObject.name== $"RakugakiObject_{drawCount-1}")
                {
                    Destroy(child.gameObject);
                    drawCount--;
                    break;
                }
            }
        }
        public void AllEraseDraw()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
            drawCount=0;
        }

        public void SetRender()
        {
            if (_controller) Destroy(_controller);
            if (VRPointerPatch.Instance != null)
            {
                var pointer = VRPointerPatch.Instance;
                _controller = pointer.gameObject.AddComponent<RakugakiRender>();
                _controller.init(this);
            }
        }
        public void UnsetRender()
        {
            if (_controller)
                Destroy(_controller.GetComponent<RakugakiRender>());
                
        }
    }
}
