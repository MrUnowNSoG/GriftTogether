using UnityEngine;

namespace GriftTogether {

    public class MapSessionControllerPresenter : IPresenter {

        private GameObject _root;
        private MapSessionControllerUIView _view;

        public MapSessionControllerPresenter(GameObject root) {
            _root = root;
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.SessionTurnView, _root).GetComponent<MapSessionControllerUIView>();
            _view.Initialize(this);
        }

        public void TurnButton() {

        }

        public void ShowUI(string message) {
            _view.UpdateTurnMessage(message);
            _view.ShowUI();
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
