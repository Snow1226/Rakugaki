using UnityEngine;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Rakugaki.Configuration;

namespace Rakugaki.UI
{
    public class DrawClearConfirm : BSMLAutomaticViewController
    {
        public ModMainFlowCoordinator mainFlowCoordinator { get; set; }

        [UIAction("OnClickYesButton")]
        private void OnClickYesButton()
        {
            Plugin.instance.rakugakiController.AllEraseDraw();
            mainFlowCoordinator.BackRakugakiUI();
        }
        [UIAction("OnClickNoButton")]
        private void OnClickNoButton()
        {
            mainFlowCoordinator.BackRakugakiUI();
        }
    }
}
