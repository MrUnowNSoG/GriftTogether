using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCManager : BaseSceneManager {
        
        private ServiceLocator _serviceLocator;
        private MapManager _mapManager;
        private PlaygroundManager _playgroundManager;

        private GameObject _root;

        private MapPhotonTurnService _turnService;
        private MapSwitchCameraService _switchCameraService;

        private MapRPCPresenter _presenter;


        public MapPhotonRPCManager(ServiceLocator serviceLocator, MapManager mapManager, PlaygroundManager playgroundManager, GameObject root) {
            _serviceLocator = serviceLocator;
            _mapManager = mapManager;
            _playgroundManager = playgroundManager;
            _root = root;
        }

        public override void Initialize() {
            InitContext();
            InjectService();
            InitUI(_root);

            _serviceLocator.Resolve(out MapPhotonRPCService service);
            service.Initialize();
        }

        private void InitContext() {
            _serviceLocator.Resolve(out MapSwitchCameraService service);
            MapPhotonRPCContext context = new MapPhotonRPCContext(this, service);
            GameRoot.PhotonManager.CurrentPhotonContext = context;
        }

        private void InjectService() {
            _serviceLocator.Resolve(out _turnService);
            _serviceLocator.Resolve(out _switchCameraService);
        }

        private void InitUI(GameObject root) {
            _presenter = new MapRPCPresenter(root);
            _presenter.Initialize();
        }


        public void GetNextTurn() {

            _turnService.NextTurn();
            _switchCameraService.SwithcTarget(_turnService.GetCurrentPlayerIndex);

            string message = _turnService.GetCurrentName() + " " + GameRoot.LocalizationManager.Get(MapPhotonRPCManagerMessage.TURN_PLAYER);
            _presenter.ShowFadeMessage(message);

            _mapManager.GetNextTurn(_turnService.IsTurnPlayer());
        }

        public void SpawnFadeLog(string message) {
            _presenter.ShowFadeMessage(message);
        }

        public void BuyBuild(string indeficator, int playerIndex) {
            _playgroundManager.BuyBuild(indeficator, playerIndex);
        }

        public void PayRent(string indeficator) {
            _playgroundManager.PayRent(indeficator);
        }

        public void RemoveOwner(string indeficator, int playerIndex) {
            _playgroundManager.RemoveOwner(indeficator, playerIndex);
        }


        public override void Deinitialize() {
            _presenter.Deinitialize();
        }
    }
}
