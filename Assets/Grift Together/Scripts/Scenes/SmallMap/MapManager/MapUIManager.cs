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
            _menuPresenter.OnLeaveGame += LeaveGame;

            _headerPresenter = new MapHeaderPresenter(_gameUIRoot.GetHeaderParent, _playerObject, _serviceLocator);
            _headerPresenter.Initialize();

            _mapPopUpPresenter = new MapPopUpPresenter(_overlayRoot, _serviceLocator);
            _mapPopUpPresenter.Initialize();
            _mapPopUpPresenter.OnLeaveGame += LeaveGame;

            _sessionControllerPresenter = new MapSessionControllerPresenter(_mainRoot);
            _sessionControllerPresenter.Initialize();
            _sessionControllerPresenter.OnTurnProcess += StartTurnProcess;

            _mapAgentPresenter = new MapAgentPresenter(_mainRoot, _serviceLocator);
            _mapAgentPresenter.Initialize();
            _mapAgentPresenter.OnSkipAgent += SkipMapAgent;
            _mapAgentPresenter.OnLost += LoseGame;
        }



        public void StartGame(float timer) => _mapPopUpPresenter.StartGame(timer);


        private void StartTurnProcess() => _mapManager.StartTurnProcess();
        public void StopTurnProcess(string message) => _sessionControllerPresenter.ShowUI(message);


        public void ShowBuyAgent(string indeficator, PlaygroundAgentBuyData data) => _mapAgentPresenter.ShowUI(indeficator, data);
        public void ShowRentAgent(string indeficator, PlaygroundAgentRentData data) => _mapAgentPresenter.ShowUI(indeficator, data);
        public void ShowChangeAgent(string indeficator, PlaygroundAgentChangeData data) => _mapAgentPresenter.ShowUI(indeficator, data);
        public void SkipMapAgent() => _mapManager.SkipMapAgent();


        public void LoseGame() => _mapPopUpPresenter.LostGame();
        private void LeaveGame() => _mapManager.LeaveGame();



        public override void Deinitialize() {
            _mapAgentPresenter.OnLost -= LoseGame;
            _mapAgentPresenter.OnSkipAgent -= SkipMapAgent;
            _mapAgentPresenter.Deinitialize();

            _mapPopUpPresenter.OnLeaveGame += LeaveGame;
            _mapAgentPresenter.Deinitialize();

            _sessionControllerPresenter.OnTurnProcess -= StartTurnProcess;
            _sessionControllerPresenter.Deinitialize();

            _menuPresenter.OnLeaveGame += LeaveGame;
            _menuPresenter.Deinitialize();

            GameRoot.PrefabManager.DestroyGameObject(_gameUIRoot.gameObject);
        }
    }
}
