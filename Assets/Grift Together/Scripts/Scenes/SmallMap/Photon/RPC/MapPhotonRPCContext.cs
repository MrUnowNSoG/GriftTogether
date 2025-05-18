using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCContext : PhotonRPCContext {
        
        private MapPhotonRPCManager _mapRPCManager;
        public MapPhotonRPCManager GetRPCManage => _mapRPCManager;

        private MapSwitchCameraService _cameraService;
        public MapSwitchCameraService GetCameraSwitcher => _cameraService;
        
        public MapPhotonRPCContext(MapPhotonRPCManager manager,  MapSwitchCameraService cameraService) {
            _mapRPCManager = manager;
            _cameraService = cameraService;
        }
    }
}
