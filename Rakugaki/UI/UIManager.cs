using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;

namespace Rakugaki.UI
{
    internal static class UIManager
    {
        internal static FlowCoordinator _mainFlow { get; private set; }
        public static void Init()
        {

            MenuButton menuButton = new MenuButton("Rakugaki", "scribble mod", ShowModFlowCoordinator, true);
            MenuButtons.Instance.RegisterButton(menuButton);
        }

        public static void ShowModFlowCoordinator()
        {
            if (Plugin.instance.rakugakiController.mainFlowCoordinator == null)
                Plugin.instance.rakugakiController.mainFlowCoordinator = BeatSaberUI.CreateFlowCoordinator<ModMainFlowCoordinator>();
            if (Plugin.instance.rakugakiController.mainFlowCoordinator.IsBusy) return;

            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(Plugin.instance.rakugakiController.mainFlowCoordinator);
        }
    }
}
