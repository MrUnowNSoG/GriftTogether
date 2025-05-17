using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCManager : BaseSceneManager {
        
        private ServiceLocator _serviceLocator;
        private MapUIManager _mapUIManager;
        private MapGameManager _mapGameManager;

        private GameObject _root;

        private MapPhotonTurnService _turnService;

        private MapRPCPresenter _presenter;


        public MapPhotonRPCManager(ServiceLocator serviceLocator, MapUIManager mapUIManager, MapGameManager mapGameManager, GameObject root) {
            _serviceLocator = serviceLocator;
            _mapUIManager = mapUIManager;
            _mapGameManager = mapGameManager;
            _root = root;
        }

        public override void Initialize() {
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
            
            _mapUIManager.GetNextTurn(_turnService.IsTurnPlayer());
            _mapGameManager.GetNextTurn(_turnService.IsTurnPlayer());
        }


        public override void Deinitialize() {
            _presenter.Deinitialize();
        }
    }
}
