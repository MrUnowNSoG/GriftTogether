using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace GriftTogether {

    public class LobbyMenuPresenter : IPresenter {


        private GameObject _root;
        private Button _openMenuButton;
        private Button _closeMenuButton;
        private ServiceLocator _serviceLocator;
        private DustService _dustService;

        private LobbyMenuUiView _view;

        public LobbyMenuPresenter(GameObject root, Button openButton, Button closeButton, ServiceLocator serviceLocator) {
            _root = root;
            _openMenuButton = openButton;
            _closeMenuButton = closeButton;
            _serviceLocator = serviceLocator;
            _serviceLocator.Resolve(out _dustService);
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LobbyPrefabType.Menu, _root).GetComponent<LobbyMenuUiView>();
            _view.Initialize(this);
            _view.ShowCurrentDust(_dustService.ToString());
            InitUI();
        } 

        private void  InitUI() {
            _openMenuButton.onClick.AddListener(ShowUI);
            _closeMenuButton.onClick.AddListener(CloseUI);

            CloseUI(); 
        }

        public void StartLobby() {
            CloseUI();

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            _serviceLocator.Resolve(out LobbyPhotonRPCService service);
            service.StartSessionGame();
        }

        public void ChangeDust() {
            if (PhotonNetwork.IsMasterClient == false) return;

            _serviceLocator.Resolve(out LobbyPhotonRPCService service);
            (int i1, int i2, int i3) = _dustService.RandomDust();
            service.ChangeDust(i1, i2, i3);
        }

        public void ChangeUiDust(string dust) => _view.ShowCurrentDust(dust);

        public void LeaveLobby() {
            CloseUI();
            PhotonNetwork.LeaveRoom();
        }


        public void ShowUI() {
            _openMenuButton.gameObject.SetActive(false);
            _closeMenuButton.gameObject.SetActive(true);

            _view.ShowUI();
        }

        public void CloseUI() {
            _openMenuButton.gameObject.SetActive(true);
            _closeMenuButton.gameObject.SetActive(false);
        
            _view.CloseUI();
        }

        public void Deinitialize() {
            _closeMenuButton.onClick.RemoveListener(CloseUI);
            _openMenuButton.onClick.RemoveListener(ShowUI);

            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_root.gameObject);
        }
    }
}
