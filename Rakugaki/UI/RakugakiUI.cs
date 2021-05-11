using UnityEngine;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Rakugaki.Configuration;

namespace Rakugaki.UI
{
    public class RakugakiUI : BSMLAutomaticViewController
    {
        public ModMainFlowCoordinator mainFlowCoordinator { get; set; }
        public void SetMainFlowCoordinator(ModMainFlowCoordinator mainFlowCoordinator)
        {
            this.mainFlowCoordinator = mainFlowCoordinator;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            Plugin.instance.rakugakiController.SetRender();
            base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
        }
        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            Plugin.instance.rakugakiController.UnsetRender();
            base.DidDeactivate(removedFromHierarchy, screenSystemDisabling);
        }
        [UIValue("DrawDelay")]
        private float DrawDelay = PluginConfig.Instance.DelayToStartDrawing;
        [UIAction("OnDrawDelayChange")]
        private void OnDrawDelayChange(float value)
        {
            DrawDelay = value;
            PluginConfig.Instance.DelayToStartDrawing = value;
        }
        [UIAction("OnDrawClear")]
        private void OnDrawClear()
        {
            Plugin.instance.rakugakiController.AllEraseDraw();
        }
        [UIAction("OnDrawUndo")]
        private void OnDrawUndo()
        {
            Plugin.instance.rakugakiController.UndoDraw();
        }
        [UIValue("DrawSize")]
        private int DrawSize = 1;
        [UIAction("OnDrawSizeChange")]
        private void OnDrawSizeChange(int value)
        {
            DrawSize = value;
            Plugin.instance.penSize = (float)value / 100;
        }
        [UIValue("DrawColor")]
        private Color DrawColor=Color.white;
        [UIAction("OnColorChange")]
        private void OnColorChange(Color value)
        {
            Plugin.instance.penColor = DrawColor = value;
        }
        [UIValue("SaveState")]
        private bool SaveState = PluginConfig.Instance.SaveDrawState;
        [UIAction("OnSaveStateChange")]
        private void OnSaveStateChange(bool value)
        {
            PluginConfig.Instance.SaveDrawState = SaveState = value;
        }
    }
}
