using UnityEngine;

namespace GriftTogether {
    public class LobbyManager : BaseSceneManager {

        private GameObject _mainGO;
        private GameObject _overlayGO;
        private ServiceLocator _serviceLocator;
        private DustService _dustService;

        private LobbyRootUIView _root;
        private LobbyHeaderPresenter _headerPresenter;
        private LobbyMenuPresenter _menuPresenter;
        private LobbyPopUpPresenter _popupPresenter;

        public LobbyManager(GameObject main, GameObject overlay, ServiceLocator serviceLocator) { 
            _mainGO = main;
            _overlayGO = overlay;
            _serviceLocator = serviceLocator;
            _serviceLocator.Resolve(out _dustService);
        }

        public override void Initialize() {
            _root = GameRoot.PrefabManager.InstantiatePrefab(LobbyPrefabType.UIRoot, _mainGO).GetComponent<LobbyRootUIView>();
            
            _headerPresenter = new LobbyHeaderPresenter(_root.GetHeaderParent);
            _headerPresenter.Initialize();

            _menuPresenter = new LobbyMenuPresenter(_root.GetLeftCornerParent, _root.GetOpenMenuButton, _root.GetCloseMenuButton, _serviceLocator);
            _menuPresenter.Initialize();

            _popupPresenter = new LobbyPopUpPresenter(_overlayGO);
            _popupPresenter.Initialize();
        }

        public void StartGameTimerRPC(float timer) {
            _menuPresenter.CloseUI();
            _root.GetCloseMenuButton.gameObject.SetActive(false);
            _root.GetOpenMenuButton.gameObject.SetActive(false);
            _popupPresenter.ShowTimerPopUp(timer);
        }

        public void ChangeDustRPC(int i1, int i2, int i3) {
            _dustService.ChangeDust(i1, i2, i3);
            _popupPresenter.ChangeDust(_dustService.ChangeDustMessage(), _dustService.ToString());
            _menuPresenter.ChangeUiDust(_dustService.ToString());
        }

        public override void Deinitialize() {

            _menuPresenter.Deinitialize();
            _headerPresenter.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_root.gameObject);
        }
        
    }
}
