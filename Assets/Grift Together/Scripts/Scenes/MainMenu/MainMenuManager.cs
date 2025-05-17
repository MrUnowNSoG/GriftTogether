using UnityEngine;

namespace GriftTogether {
    public class MainMenuManager : BaseSceneManager {

        private GameObject _mainCanvas;
        private GameObject _overlayCanvas;
        private ServiceLocator _serviceLocator;

        private MainMenuRootUIView _mainMenuRootUIView;
        private MainMenuHeaderPresenter _mainMenuHeaderPresenter;
        private MainMenuPresenter _mainMenuPresenter;

        public MainMenuManager(GameObject mainCanvas, GameObject overlayCanvas, ServiceLocator serviceLocator) {
            _mainCanvas = mainCanvas;
            _overlayCanvas = overlayCanvas;
            _serviceLocator = serviceLocator;
        }

        public override void Initialize() {

            _mainMenuRootUIView = GameRoot.PrefabManager.InstantiatePrefab(MainMenuPrefabType.RootView, _mainCanvas).GetComponent<MainMenuRootUIView>();

            _mainMenuHeaderPresenter = new MainMenuHeaderPresenter(_mainMenuRootUIView.GetHeaderParent);
            _mainMenuHeaderPresenter.Initialize();

            _mainMenuPresenter = new MainMenuPresenter(_mainMenuRootUIView.GetLeftCornerParent, _overlayCanvas, _serviceLocator);
            _mainMenuPresenter.Initialize();   
        }


        public override void Deinitialize() {
            _mainMenuPresenter.Deinitialize();
            _mainMenuHeaderPresenter.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_mainMenuRootUIView.gameObject);
        }
    }
}
