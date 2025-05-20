using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class MapUIManager : BaseSceneManager {

        private MapManager _mapManager;
        private GameObject _mainRoot;
        private GameObject _overlayRoot;

        private MapPlayerObject _playerObject;
        private ServiceLocator _serviceLocator;

        private MapRootUIGameView _gameUIRoot;
        private MapHeaderPresenter _headerPresenter;
        private MapMenuPresenter _menuPresenter;

        private MapPopUpPresenter _mapPopUpPresenter;

        private MapSessionControllerPresenter _sessionControllerPresenter;
        private MapAgentPresenter _mapAgentPresenter;

        
        public MapUIManager(MapManager map, GameObject mainRoot, GameObject overlayRoot, MapPlayerObject playerObject, ServiceLocator service) {
            _mapManager = map;
            _mainRoot = mainRoot;
            _overlayRoot = overlayRoot;

            _playerObject = playerObject;
            _serviceLocator = service;
        }

        public override void Initialize() {
            _gameUIRoot = GameRoot.PrefabManager.InstantiatePrefab(MapPrefabType.GameUIRoot, _mainRoot).GetComponent<MapRootUIGameView>();

            _serviceLocator.Resolve(out AnalyticsService service);
            _menuPresenter = new MapMenuPresenter(_overlayRoot, _gameUIRoot.GetLeftCornerParent, service);
            _menuPresenter.Initialize();

            _headerPresenter = new MapHeaderPresenter(_gameUIRoot.GetHeaderParent, _playerObject, _serviceLocator);
            _headerPresenter.Initialize();

            _mapPopUpPresenter = new MapPopUpPresenter(_overlayRoot, _serviceLocator);
            _mapPopUpPresenter.Initialize();

            _sessionControllerPresenter = new MapSessionControllerPresenter(_mainRoot);
            _sessionControllerPresenter.Initialize();
            _sessionControllerPresenter.OnTurnProcess += StartTurnProcess;

            _mapAgentPresenter = new MapAgentPresenter(_mainRoot, _serviceLocator);
            _mapAgentPresenter.Initialize();
            _mapAgentPresenter.OnSkipAgent += SkipMapAgent;
            _mapAgentPresenter.OnLost += Lost;
        }


        public void StartGame(float timer) => _mapPopUpPresenter.StartGame(timer);

        private void StartTurnProcess() => _mapManager.StartTurnProcess();
        public void StopTurnProcess(string message) => _sessionControllerPresenter.ShowUI(message);

        public void ShowBuyAgent(string indefictor, PlaygroundAgentBuyData data) => _mapAgentPresenter.ShowUI(indefictor, data);
        public void ShowRentAgent(string indefictor, PlaygroundAgentRentData data) => _mapAgentPresenter.ShowUI(indefictor, data);
        public void SkipMapAgent() => _mapManager.SkipMapAgent();

        public void Lost() {
            PhotonNetwork.LeaveRoom();
            _mapPopUpPresenter.LostGame();
        }


        public override void Deinitialize() {

            _mapAgentPresenter.OnLost -= Lost;

            _mapAgentPresenter.OnSkipAgent -= SkipMapAgent;
            _mapAgentPresenter.Deinitialize();

            _sessionControllerPresenter.OnTurnProcess -= StartTurnProcess;
            _sessionControllerPresenter.Deinitialize();

            _menuPresenter.Deinitialize();

            GameRoot.PrefabManager.DestroyGameObject(_gameUIRoot.gameObject);
        }
    }
}
