using UnityEngine;

namespace GriftTogether {

    public class MapRPCPresenter : IPresenter {

        private GameObject _root;

        private MapRPCUiView _view;

        public MapRPCPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.RPCViem, _root).GetComponent<MapRPCUiView>();
            _view.Initialize(this);
        }

        public void ShowFadeMessage(string message) => _view.ShowFadeLog(message);

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
