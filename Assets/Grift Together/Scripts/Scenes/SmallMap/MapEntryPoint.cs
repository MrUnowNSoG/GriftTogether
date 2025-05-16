using UnityEngine;

namespace GriftTogether {

    public class MapEntryPoint : BaseEntryPoint {

        [Space(0)][Header("Manage")]
        [SerializeField] private MapPhotonManager _photonManager;
        [SerializeField] private MapPhotonRPCManager _rpcManager;
        [SerializeField] private PlaygroundManager _groundManager;
        [SerializeField] private MapManager _mapManager;

        [Space(10)][Header("Service")]
        [SerializeField] private MapPhotonTurnService _turnService;

        private void Awake() {
            Initialize(GameRoot.ServiceLocator);
        }

        public override void Initialize(ServiceLocator parent) {
            base.Initialize(parent);
            RegisterGameServices();
            RegisterSceneServices();
            InitSceneManager();
        }

        protected override void RegisterGameServices() {}
        protected override void RegisterSceneServices() {
        
            if(_localServiceLocator.Resolve(out _turnService) == false) {
                _localServiceLocator.AddService(_turnService);
            }
            _turnService.Init(_localServiceLocator);

        }

        protected override void InitSceneManager() {

            _mapManager = new MapManager();
            _rpcManager = new MapPhotonRPCManager();

            _groundManager.Init();
            _photonManager.Init(_localServiceLocator, _groundManager, _mapManager);
            _mapManager.Init();
            _rpcManager.Init(_localServiceLocator, _mapManager);

            _photonManager.PlayerReady();
        }


        public override void Deinitialize() {
        }

    }
}
