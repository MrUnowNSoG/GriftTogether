using Photon.Pun;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPhotonRPCService : MonoBehaviour, IService {

        private const float TIME_FOR_START_GAME = 5f;

        private PhotonView _photonView;
        private LobbyPhotonRPCContext _context;

        private void Start() {
            _photonView = gameObject.GetComponent<PhotonView>();
        }

        public void StartSessionGame() {
            _photonView.RPC(nameof(RPC_StartGameCountdown), RpcTarget.All, PhotonNetwork.Time + TIME_FOR_START_GAME);
        }

        [PunRPC]
        private void RPC_StartGameCountdown(double endTime) {
            if (_context == null) InitContext();
            _context.LobbyManager.StartGameTimerRPC((float)(endTime - PhotonNetwork.Time));
        }


        public void ChangeDust(int i1, int i2, int i3) {
            _photonView.RPC(nameof(RPC_ChangeDust), RpcTarget.All, i1, i2, i3);
        }

        [PunRPC]
        private void RPC_ChangeDust(int i1, int i2, int i3) {
            if (_context == null) InitContext();
            _context.LobbyManager.ChangeDustRPC(i1, i2, i3);
        }

        private void InitContext() {
            LobbyPhotonRPCContext context = GameRoot.PhotonManager.CurrentPhotonContext as LobbyPhotonRPCContext;

            if (context != null) {
                _context = context;

            } else {
                Debug.LogError("CRITICAL ERROR! Can't research LobbyPhotonRPCContext!");
                return;
            }
        }
    }
}
