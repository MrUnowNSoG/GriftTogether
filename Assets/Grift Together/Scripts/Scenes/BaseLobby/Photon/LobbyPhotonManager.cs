using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPhotonManager : MonoBehaviourPunCallbacks {

        [SerializeField] private BaseEntryPoint _lobbyEntryPoint;

        private bool _initAvatar = false;

        private void Start() {
            if (PhotonNetwork.InRoom && _initAvatar == false) {
                CreateAvatar(PhotonNetwork.LocalPlayer);
                CreateRPCAgent();
            }
        }

        public override void OnJoinedRoom() {
            if (_initAvatar == false) {
                CreateAvatar(PhotonNetwork.LocalPlayer);
                CreateRPCAgent();
                Debug.Log("OnJoinedRoom: " + PhotonNetwork.LocalPlayer.NickName);
            }
        }

        private void CreateAvatar(Player p) {
            _initAvatar = true;
            GameRoot.ServiceLocator.Resolve(out SkinPhotonService service);
            service.CreatePhotonPlayer(p);
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
