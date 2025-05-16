using Photon.Pun;
using UnityEngine;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;

namespace GriftTogether {

    public class MapPhotonRPCService : MonoBehaviour, IService {


        private PhotonView _view;
        private MapPhotonRPCManager _manager;

        public void Init() {
            _view = GetComponent<PhotonView>();
        }


        public void RPC_SendNextTurn() {
            _view.RPC(nameof(RPC_GetNextTutn),RpcTarget.All);
        }


        [PunRPC]
        public void RPC_GetNextTutn() {
            if (_manager == null) InitManager();
            _manager.GetNextTurn();
        }

        private void InitManager() {

            MapPhotonRPCContext context = GameRoot.PhotonManager.CurrentPhotonContext as MapPhotonRPCContext;

            if (context != null) {
                _manager = context.MapRPCManager;

            } else {
                Debug.LogError("CRITICAL ERROR! Can't research MapPhotonRPCContext!");
                return;
            }
        }
    }
}
