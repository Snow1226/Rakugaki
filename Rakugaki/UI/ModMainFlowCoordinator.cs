using BeatSaberMarkupLanguage;
using HMUI;
using Rakugaki.Configuration;

namespace Rakugaki.UI
{
    public class ModMainFlowCoordinator : FlowCoordinator
    {
        private const string titleString = "Rakugaki";
        private RakugakiUI rakugakiUI;
        private DrawClearConfirm drawClearConfirm;
        public bool IsBusy { get; set; }

        public void ShowRakugakiUI()
        {
            this.IsBusy = true;
            this.SetTopScreenViewController(this.rakugakiUI, ViewController.AnimationType.In);
            this.IsBusy = false;
        }

        public void ShowConfirmUI()
        {
            this.IsBusy = true;
            this.ReplaceTopViewController(this.drawClearConfirm, null, ViewController.AnimationType.In);
            this.IsBusy = false;
        }
        public void BackRakugakiUI()
        {
            this.IsBusy = true;
            this.ReplaceTopViewController(this.rakugakiUI, null, ViewController.AnimationType.In);
            this.IsBusy = false;
        }

        private void Awake()
        {
            this.rakugakiUI = BeatSaberUI.CreateViewController<RakugakiUI>();
            this.rakugakiUI.mainFlowCoordinator = this;
            this.drawClearConfirm = BeatSaberUI.CreateViewController<DrawClearConfirm>();
            this.drawClearConfirm.mainFlowCoordinator = this;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            SetTitle(titleString);
            this.showBackButton = true;

            var viewToDisplay = DecideMainView();

            this.IsBusy = true;
            ProvideInitialViewControllers(viewToDisplay);
            this.IsBusy = false;
        }

        private ViewController DecideMainView()
        {
            ViewController viewToDisplay;
            viewToDisplay = this.rakugakiUI;
            return viewToDisplay;
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            if (this.IsBusy) return;
            if(PluginConfig.Instance.SaveDrawState)
                Plugin.instance.rakugakiController.SaveDrawData();
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
