using BeatSaberMarkupLanguage;
using HMUI;

namespace Rakugaki.UI
{
    public class ModMainFlowCoordinator : FlowCoordinator
    {
        private const string titleString = "Rakugaki";
        private RakugakiUI rakugakiUI;

        public bool IsBusy { get; set; }

        public void ShowRakugakiUI()
        {
            this.IsBusy = true;
            this.SetLeftScreenViewController(this.rakugakiUI, ViewController.AnimationType.In);
            this.IsBusy = false;
        }

        private void Awake()
        {
            this.rakugakiUI = BeatSaberUI.CreateViewController<RakugakiUI>();
            this.rakugakiUI.mainFlowCoordinator = this;
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

            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
