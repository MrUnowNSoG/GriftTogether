using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCManager : BaseSceneManager {
        
        private ServiceLocator _serviceLocator;
        private MapManager _mapManager;
        private GameObject _root;

        private MapPhotonRPCService _rpcService;
        private MapPhotonTurnService _turnService;

        private MapRPCPresenter _presenter;


        public MapPhotonRPCManager(ServiceLocator serviceLocator, MapManager mapManager, GameObject root) {
            _serviceLocator = serviceLocator;
            _mapManager = mapManager;
            _root = root;
        }

        public override void Init() {
            InitContext();
            InjectService();
            InitUI(_root);
        }

        private void InitContext() {
            MapPhotonRPCContext context = new MapPhotonRPCContext();
            context.MapRPCManager = this;
            GameRoot.PhotonManager.CurrentPhotonContext = context;
        }

        private void InjectService() {
            _serviceLocator.Resolve(out _rpcService);
            _serviceLocator.Resolve(out _turnService);
        }

        private void InitUI(GameObject root) {
            _presenter = new MapRPCPresenter(root);
            _presenter.Initialize();
        }


        public void GetNextTurn() {
            _turnService.NextTurn();

            string message = _turnService.GetCurrentName() + " " + GameRoot.LocalizationManager.Get(MapPhotonRPCManagerMessage.TURN_PLAYER);
            _presenter.ShowFadeMessage(message);
            
            _mapManager.GetNextTurn(_turnService.IsTurnPlayer());
        }


        public override void Deinitialize() {
            _presenter.Deinitialize();
        }
    }
}
