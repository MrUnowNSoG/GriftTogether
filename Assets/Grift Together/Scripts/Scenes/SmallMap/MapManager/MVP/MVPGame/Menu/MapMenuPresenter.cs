using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class MapMenuPresenter : IPresenter {

        private GameObject _overlayRoot;
        private GameObject _root;

        private AnalyticsService _analyticsService;

        private MapMenuUIView _menuUIView;
        private MapMenuBugUIView _bugView;

        public MapMenuPresenter(GameObject overlayRoot, GameObject root, AnalyticsService analitic) {
            _overlayRoot = overlayRoot;
            _root = root;
            _analyticsService = analitic;
        }

        public void Initialize() {
            _menuUIView = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameMenuView, _root).GetComponent<MapMenuUIView>();
            _menuUIView.Initialize(this);
            _menuUIView.CloseUI();

            _bugView = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameBugView, _overlayRoot).GetComponent<MapMenuBugUIView>();
            _bugView.Initialize(this);
        }

        public void ShowBugReport() {
            CloseUI();
            _bugView.ShowUI();
        }

        public void SendBug(string header, string description) {
            _analyticsService.SendBugRepost(header, description);
            _bugView.CloseUI();
        }

        public void LeaveGame() {
            _menuUIView.CloseUI();
            GameRoot.ScenesManager.ShowLoadingScreen();
            PhotonNetwork.LeaveRoom();
        }


        public void ShowUI() => _menuUIView.ShowUI();
        public void CloseUI() => _menuUIView.CloseUI();

        public void Deinitialize() {
            _bugView.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_bugView.gameObject);

            _menuUIView.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_menuUIView.gameObject);
        }
    }
}
