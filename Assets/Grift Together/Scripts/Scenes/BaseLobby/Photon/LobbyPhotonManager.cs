using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPhotonManager : MonoBehaviourPunCallbacks {

        [SerializeField] private BaseEntryPoint _lobbyEntryPoint;

       
        private void Start() {
            CreateAvatar(PhotonNetwork.LocalPlayer);
            CreateRPCAgent();
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
