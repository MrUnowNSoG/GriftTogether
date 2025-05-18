using UnityEngine;

namespace GriftTogether {

    public class MapHeaderPresenter : IPresenter {

        private GameObject _root;

        private MapPlayerObject _player;
        private MapPhotonTurnService _turnService;

        private MapHeaderUIView _view;

        public MapHeaderPresenter(GameObject root, MapPlayerObject mapPlayerObject, ServiceLocator service) {
            _root = root;

            _player = mapPlayerObject;
            service.Resolve(out _turnService);
        }

        public void Initialize() {
            _view = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameHeaderView, _root).GetComponent<MapHeaderUIView>();
            _view.Initilize(this, _player, _turnService);
        }

        public void ShowUI() => _view.ShowUI();
        public void CloseUI() => _view.CloseUI();

        public void Deinitialize() {
            _view.Deinitialize();
            GameRoot.PrefabManager.DestroyGameObject(_view.gameObject);
        }
    }
}
