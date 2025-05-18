using UnityEngine;

namespace GriftTogether {

    public class MapUIManager : BaseSceneManager {

        private MapManager _mapManager;
        private GameObject _mainRoot;

        private MapRootUIGameView _gameUIRoot;
        private MapMenuPresenter _menuPresenter;

        private MapSessionControllerPresenter _sessionControllerPresenter;

        public MapUIManager(MapManager map, GameObject main) {
            _mapManager = map;
            _mainRoot = main;
        }

        public override void Initialize() {
            _gameUIRoot = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameUIRoot, _mainRoot).GetComponent<MapRootUIGameView>();

            _menuPresenter = new MapMenuPresenter(_gameUIRoot.GetLeftCornerParent);
            _menuPresenter.Initialize();

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
