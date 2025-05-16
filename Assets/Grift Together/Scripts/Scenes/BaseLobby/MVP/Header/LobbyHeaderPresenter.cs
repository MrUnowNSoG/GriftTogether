using UnityEngine;

namespace GriftTogether {

    public class LobbyHeaderPresenter : IPresenter {

        private GameObject _root;

        private LobbyHeaderUiView _view;

        public LobbyHeaderPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(LobbyPrefabType.Header, _root).GetComponent<LobbyHeaderUiView>();
            _view.Initialize(this);
            _view.UpdateData(GameRoot.PlayerGlobalManager.GetCountWin, GameRoot.PlayerGlobalManager.GetCountCoin);
        }

        public void ShowUI() {}

        public void CloseUI() {}

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_root.gameObject);
        }
    }
}
