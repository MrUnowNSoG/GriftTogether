using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GriftTogether {
    public class SkinPhotonService : SkinService {
    
        public void CreatePhotonPlayer(Player player) {

            if (player.CustomProperties.TryGetValue(PhotonManagerConst.HAT_SKIN_KEY, out var hatSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.COLOR_SKIN_KEY, out var colorSkin) &&
                player.CustomProperties.TryGetValue(PhotonManagerConst.FACE_SKIN_KEY, out var faceSkin)) {

                Vector3 spawnPos = Vector3.up * 10f;

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

    }
}
