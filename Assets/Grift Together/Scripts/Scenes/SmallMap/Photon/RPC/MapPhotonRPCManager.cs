using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCManager {
        
        private ServiceLocator _serviceLocator;
        private MapPhotonRPCService _rpcService;
        private MapPhotonTurnService _turnService;

        private MapManager _mapManager;

        public void Init(ServiceLocator serviceLocator, MapManager mapManager) {

            MapPhotonRPCContext context = new MapPhotonRPCContext();
            context.MapRPCManager = this;
            GameRoot.PhotonManager.CurrentPhotonContext = context;
        

            _serviceLocator = serviceLocator;
            _serviceLocator.Resolve(out _rpcService);
            _serviceLocator.Resolve(out _turnService);

            _mapManager = mapManager;
        }

        private void InitUI() {

        }

        public void GetNextTurn() {
            _turnService.NextTurn();
            _mapManager.GetNextTurn(_turnService.IsTurnPlayer());
        }
    }
}
