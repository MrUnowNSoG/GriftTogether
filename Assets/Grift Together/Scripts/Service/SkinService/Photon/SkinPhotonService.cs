using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {
    public class SkinPhotonService : SkinService {

        private float HIGHT_SPAWN_POS = 10f;
        private float RANGE_SPAWN_POS = 5f;
    
        public void CreatePhotonLobbyPlayer(Player player) {

            if (player.CustomProperties.TryGetValue(PhotonManagerConst.HAT_SKIN_KEY, out var hatSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.COLOR_SKIN_KEY, out var colorSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.FACE_SKIN_KEY, out var faceSkin)) {

                Vector3 spawnPos = new Vector3(Random.Range(-RANGE_SPAWN_POS, RANGE_SPAWN_POS), 
                                               HIGHT_SPAWN_POS,
                                               Random.Range(-RANGE_SPAWN_POS, RANGE_SPAWN_POS));

                SkinServicePhotonAgent agent = PhotonNetwork.Instantiate(PhotonPrefabConst.LOBBY_PLAYER_PREFAB_PATH, 
                                                                        spawnPos, 
                                                                        Quaternion.identity, 
                                                                        0, 
                                                                        new object[] { hatSkin, colorSkin, faceSkin }).
                                                                        GetComponent<SkinServicePhotonAgent>();

                agent.SetUserName(PhotonNetwork.LocalPlayer.NickName);
                SetSkinAgent(agent);
                RecreatePlayerSkin((string)hatSkin, (string)colorSkin, (string)faceSkin);
                ResolveCurrentSkin();


            } else {
                Debug.LogWarning($"No avatar props for {player.NickName}");
            }
        }

        public MapPlayerObject CreatePhotonMapPlayer(Player player, Vector3 pos) {

            if (player.CustomProperties.TryGetValue(PhotonManagerConst.HAT_SKIN_KEY, out var hatSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.COLOR_SKIN_KEY, out var colorSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.FACE_SKIN_KEY, out var faceSkin)) {

                GameObject playerObject = PhotonNetwork.Instantiate(PhotonPrefabConst.MAP_PLAYER_PREFAB_PATH,
                                                                        pos,
                                                                        Quaternion.identity,
                                                                        0,
                                                                        new object[] { hatSkin, colorSkin, faceSkin });

                SkinServicePhotonAgent agent = playerObject.GetComponent<SkinServicePhotonAgent>();
                agent.SetUserName(PhotonNetwork.LocalPlayer.NickName);
                SetSkinAgent(agent);
                RecreatePlayerSkin((string)hatSkin, (string)colorSkin, (string)faceSkin);
                ResolveCurrentSkin();

                return playerObject.GetComponent<MapPlayerObject>();

            } else {
                Debug.LogWarning($"No avatar props for {player.NickName}");
            }

            return null;
        }

    }
}
