using Photon.Pun;
using System.Threading.Tasks;
using UnityEngine;

namespace GriftTogether {

    public class MapPhotonRPCService : MonoBehaviour, IService {

        private PhotonView _view;
        private MapPhotonRPCManager _manager;

        private void Start() {
            Initialize();
        }

        public void Initialize() {

            _view = GetComponent<PhotonView>();

            MapPhotonRPCContext context = GameRoot.PhotonManager.CurrentPhotonContext as MapPhotonRPCContext;

            if (context != null) {
                _manager = context.GetRPCManage;

            } else {
                Debug.LogError("CRITICAL ERROR! Can't research MapPhotonRPCContext!");
                return;
            }
        }


        public void RPC_SendNextTurn() {
            _view.RPC(nameof(RPC_GetNextTurn),RpcTarget.All);
        }

        [PunRPC]
        public void RPC_GetNextTurn() {
            _manager.GetNextTurn();
        }


        public async void RPC_SendLog(string message) {
           _view.RPC(nameof(RPC_GetLog), RpcTarget.All, message);
        }

        [PunRPC]
        public void RPC_GetLog(string message) {
            _manager.SpawnFadeLog(message);
        }
    }
}
