using UnityEngine;

namespace GriftTogether {

    public class MapUIManager : BaseSceneManager {

        private MapManager _mapManager;
        private GameObject _mainRoot;
        private MapPlayerObject _playerObject;
        private ServiceLocator _serviceLocator;

        private MapRootUIGameView _gameUIRoot;
        private MapHeaderPresenter _headerPresenter;
        private MapMenuPresenter _menuPresenter;

        private MapSessionControllerPresenter _sessionControllerPresenter;

        public MapUIManager(MapManager map, GameObject root, MapPlayerObject playerObject, ServiceLocator service) {
            _mapManager = map;
            _mainRoot = root;
            _playerObject = playerObject;
            _serviceLocator = service;
        }

        public override void Initialize() {
            _gameUIRoot = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameUIRoot, _mainRoot).GetComponent<MapRootUIGameView>();

            _menuPresenter = new MapMenuPresenter(_gameUIRoot.GetLeftCornerParent);
            _menuPresenter.Initialize();

            _headerPresenter = new MapHeaderPresenter(_gameUIRoot.GetHeaderParent, _playerObject, _serviceLocator);
            _headerPresenter.Initialize();

            _sessionControllerPresenter = new MapSessionControllerPresenter(_mainRoot);
            _sessionControllerPresenter.Initialize();
            _sessionControllerPresenter.OnTurnProcess += StartTurnProcess;
        }

        private void StartTurnProcess() => _mapManager.StartTurnProcess();
        public void StopTurnProcess(string message) => _sessionControllerPresenter.ShowUI(message);


        public override void Deinitialize() {
            _sessionControllerPresenter.OnTurnProcess -= StartTurnProcess;
            _sessionControllerPresenter.Deinitialize();

            _menuPresenter.Deinitialize();

            GameRoot.PrefabManager.DestroyGameObject(_gameUIRoot.gameObject);
        }
    }
}
