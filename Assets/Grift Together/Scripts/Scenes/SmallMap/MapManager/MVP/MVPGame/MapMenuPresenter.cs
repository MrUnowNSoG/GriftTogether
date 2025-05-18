using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class MapMenuPresenter : IPresenter {

        private GameObject _root;
        private MapMenuUIView _menuUIView;

        public MapMenuPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _menuUIView = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameMenuView, _root).GetComponent<MapMenuUIView>();
            _menuUIView.Initialize(this);
            _menuUIView.CloseUI();
        }

        public void LeaveGame() {
            _menuUIView.CloseUI();
            PhotonNetwork.LeaveRoom();
        }

        public void ShowUI() => _menuUIView.ShowUI();
        public void CloseUI() => _menuUIView.CloseUI();

        public void Deinitialize() {
            _menuUIView.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_menuUIView.gameObject);
        }
    }
}
