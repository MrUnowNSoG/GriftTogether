using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPhotonManager : MonoBehaviourPunCallbacks {

        [SerializeField] private BaseEntryPoint _lobbyEntryPoint;

        private bool _initAwatar = false;
       
        private void Start() {
            if(_initAwatar == false && PhotonNetwork.InRoom) {
                _initAwatar = true;
                CreateAvatar(PhotonNetwork.LocalPlayer);
                CreateRPCAgent();
            }
        }

        public override void OnJoinedRoom() {
            Start();
        }

        private void CreateAvatar(Player p) {
            GameRoot.ServiceLocator.Resolve(out SkinPhotonService service);
            service.CreatePhotonLobbyPlayer(p);
        }

        private void CreateRPCAgent() {
            LobbyPhotonRPCService service = PhotonNetwork.Instantiate(PhotonPrefabConst.LOBBY_RPC_AGENT_PREFAB_PATH, Vector3.zero, Quaternion.identity).GetComponent<LobbyPhotonRPCService>();
            _lobbyEntryPoint.ExtendLocalServiceLocator(service);
        }

        public override void OnLeftRoom() {
            GameRoot.ScenesManager.ShowLoadingScreen();
            _lobbyEntryPoint.Deinitialize();
            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.MENU_SCENE, true);
        }

    }
}
