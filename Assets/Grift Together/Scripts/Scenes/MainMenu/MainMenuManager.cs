using UnityEngine;

namespace GriftTogether {
    public class MainMenuManager : BaseSceneManager {

        private Canvas _mainCanvas;
        private Canvas _overlayCanvas;

        private MainMenuRootUIView _mainMenuRootUIView;
        private MainMenuHeaderPresenter _mainMenuHeaderPresenter;
        private MainMenuPresenter _mainMenuPresenter;

        public MainMenuManager(Canvas mainCanvas, Canvas overlayCanvas) {
            _mainCanvas = mainCanvas;
            _overlayCanvas = overlayCanvas;
        }

        public override void Init() {

            _mainMenuRootUIView = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.RootView, _mainCanvas.gameObject).GetComponent<MainMenuRootUIView>();

            _mainMenuHeaderPresenter = new MainMenuHeaderPresenter(_mainMenuRootUIView.GetHeaderParent);
            _mainMenuHeaderPresenter.Initialize();

            _mainMenuPresenter = new MainMenuPresenter(_mainMenuRootUIView.GetLeftCornerParent.gameObject, _overlayCanvas.gameObject);
            _mainMenuPresenter.Initialize();   
        }


        public override void Deinitialize() {
            _mainMenuPresenter.Deinitialize();
            _mainMenuHeaderPresenter.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_mainMenuRootUIView.gameObject);
        }
    }
}
