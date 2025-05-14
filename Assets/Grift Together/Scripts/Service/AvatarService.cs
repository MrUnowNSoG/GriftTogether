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

            Debug.Log("Маємо нове Start: " + SceneManager.GetActiveScene().name);

            // PhotonNetwork.AddCallbackTarget(this);  // щоб ловити нові підключення
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            CreateAvatarFor(newPlayer);

            Debug.Log("Маємо ще 1 нове Start: " + SceneManager.GetActiveScene().name);
        }


        private void CreateAvatarFor(Player p) {

            if(_skinService == null) {
                GameRoot.ServiceLocator.Resolve(out _skinService);
            }

            if (p.CustomProperties.TryGetValue("s1", out var o1) &&
                p.CustomProperties.TryGetValue("s2", out var o2) &&
                p.CustomProperties.TryGetValue("s3", out var o3)) {
            
                SkinServiceAgent agent = GameObject.Instantiate(_agentPrefab, Vector3.up, Quaternion.identity).GetComponent<SkinServiceAgent>();
                _skinService.SetSkinAgent(agent);
                _skinService.RecreatePlayerSkin((string)o1, (string)o2, (string)o3);
                _skinService.ResolveCurrentSkin();

                Debug.Log($"New plyer with next skin: {(string)o1}, {(string)o2}, {(string)o3}");

                agent.transform.position += Vector3.left * Random.Range(0,5);
                agent.transform.position += Vector3.forward * Random.Range(0, 5);
                
            } else {
                Debug.LogWarning($"No avatar props for {p.NickName}");
            }
        }


    }
}
