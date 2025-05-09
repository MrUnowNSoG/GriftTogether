using UnityEngine;

namespace GriftTogether {

    public class MainMenuExitPresenter : IPresenter {

        private Canvas _parentCanvas;

        private MainMenuExitView _view;

        public MainMenuExitPresenter(Canvas canvas) {
            _parentCanvas = canvas;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.ExitView, _parentCanvas.gameObject).GetComponent<MainMenuExitView>();
            _view.OnExit += ExitGame;
        }

        private void ExitGame() {
            Deinitialize();
            GameRoot.ScenesManager.ShowLoadingScreen();
            Application.Quit();
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.OnExit -= ExitGame;
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
