using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

namespace GriftTogether {

    public class MainMenuSettingPresenter : IPresenter {

        private Canvas _parentCanvas;
        private ResolutionScreenServer _resolutionScreenServer;

        private MainMenuSettingView _view;

        public MainMenuSettingPresenter(Canvas canvas) {
            _parentCanvas = canvas;

            if(GameRoot.ServiceLocator.Resolve(out _resolutionScreenServer) == false) {
                _resolutionScreenServer = new ResolutionScreenServer();
                Debug.LogError("GameRoot: Service locator can't resolve: Resolution Screen server!");
            }
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.SettingView, _parentCanvas.gameObject).GetComponent<MainMenuSettingView>();
            _view.Initialize(this);
        }

        public void ShowUI() => _view.ShowUI();

        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
        }
    }
}
