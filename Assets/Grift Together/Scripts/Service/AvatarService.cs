using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GriftTogether {

    public class AvatarService : MonoBehaviourPunCallbacks {

        [SerializeField] private SkinServiceAgent _agentPrefab;
        private SkinService _skinService;

        void Start() {
            foreach (var p in PhotonNetwork.PlayerList)
                CreateAvatarFor(p);
        }

        public void OnJoinedRoom() {
            //OnJoinedRoom викликаЇтьс€ дл€ нового гравц€ Ч саме тод≥ в≥н бачить, хто вже в к≥мнат≥.
            Debug.Log("OnJoinedRoom: " + PhotonNetwork.LocalPlayer.NickName);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            //OnPlayerEnteredRoom викликаЇтьс€ у вс≥х кл≥Їнт≥в(включно з тобою) коли хтось новий п≥дключаЇтьс€.
            CreateAvatarFor(newPlayer);
            Debug.Log($"Ќовий гравець: {newPlayer.NickName}");
        }


        private void CreateAvatarFor(Player p) {

            if(_skinService == null) {
                GameRoot.ServiceLocator.Resolve(out _skinService);
            }

            if (p.CustomProperties.TryGetValue(PhotonManagerConst.HAT_SKIN_KEY, out var hatSkin) &&
                p.CustomProperties.TryGetValue(PhotonManagerConst.COLOR_SKIN_KEY, out var colorSkin) &&
                p.CustomProperties.TryGetValue(PhotonManagerConst.FACE_SKIN_KEY, out var faceSkin)) {
            
                SkinServiceAgent agent = GameObject.Instantiate(_agentPrefab, Vector3.up, Quaternion.identity).GetComponent<SkinServiceAgent>();
                _skinService.SetSkinAgent(agent);
                _skinService.RecreatePlayerSkin((string)hatSkin, (string)colorSkin, (string)faceSkin);
                _skinService.ResolveCurrentSkin();
                
                agent.transform.position += Vector3.left * Random.Range(0,5);
                agent.transform.position += Vector3.forward * Random.Range(0, 5);
                
            } else {
                Debug.LogWarning($"No avatar props for {p.NickName}");
            }
        }


    }
}
