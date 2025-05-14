using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {

    public class LobbyPhotonManager : MonoBehaviourPunCallbacks {

        private const string AGENT_PREFAB_RESOURCE_PATH = "PhotonPrefabs/LobbyAwatar";

        private SkinService _skinService;
        private bool _initAvatar = false;

        private void Start() {
            if (PhotonNetwork.InRoom && _initAvatar == false) {
                _initAvatar = true;
                CreateAvatar(PhotonNetwork.LocalPlayer);
            }
        }

        public override void OnJoinedRoom() {
            //OnJoinedRoom викликаЇтьс€ дл€ нового гравц€ Ч саме тод≥ в≥н бачить, хто вже в к≥мнат≥.
            if (_initAvatar == false) {
                _initAvatar = true;
                CreateAvatar(PhotonNetwork.LocalPlayer);
                Debug.Log("OnJoinedRoom: " + PhotonNetwork.LocalPlayer.NickName);
            }
        }

        private void CreateAvatar(Player p) {

            if (_skinService == null) {
                GameRoot.ServiceLocator.Resolve(out _skinService);
            }

            if (p.CustomProperties.TryGetValue(PhotonManagerConst.HAT_SKIN_KEY, out var hatSkin) &&
                p.CustomProperties.TryGetValue(PhotonManagerConst.COLOR_SKIN_KEY, out var colorSkin) &&
                p.CustomProperties.TryGetValue(PhotonManagerConst.FACE_SKIN_KEY, out var faceSkin)) {

                Vector3 spawnPos = Vector3.up * 10f;

                SkinServicePhotonAgent agent = PhotonNetwork.Instantiate(AGENT_PREFAB_RESOURCE_PATH, spawnPos, Quaternion.identity, 0, new object[] { hatSkin, colorSkin, faceSkin }).GetComponent<SkinServicePhotonAgent>();
                agent.SetUserName(PhotonNetwork.LocalPlayer.NickName);
                _skinService.SetSkinAgent(agent);
                _skinService.RecreatePlayerSkin((string)hatSkin, (string)colorSkin, (string)faceSkin);
                _skinService.ResolveCurrentSkin();


            } else {
                Debug.LogWarning($"No avatar props for {p.NickName}");
            }
        }



    }
}
