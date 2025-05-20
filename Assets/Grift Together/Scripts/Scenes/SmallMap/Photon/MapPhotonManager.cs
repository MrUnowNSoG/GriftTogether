using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace GriftTogether {

    public class MapPhotonManager : MonoBehaviourPunCallbacks {

        
        private ServiceLocator _serviceLocator;
        private PlaygroundManager _playground;
        private MapManager _mapManager;

        public MapPlayerObject Initialize(ServiceLocator serviceLocator, PlaygroundManager playground, MapManager mapManager) {
            _serviceLocator = serviceLocator;
            
            _playground = playground;
            _mapManager = mapManager;

            return CreateLocalPlayer();
        }

        private MapPlayerObject CreateLocalPlayer() {
            GameObject player = PhotonNetwork.Instantiate(PhotonPrefabConst.MAP_RPC_AGENT_PREFAB_PATH, Vector3.zero, Quaternion.identity);
            MapPhotonRPCService rpcService = player.GetComponent<MapPhotonRPCService>();
            _serviceLocator.AddService(rpcService);

            GameRoot.ServiceLocator.Resolve(out SkinPhotonService slinService);
            int indexPlayer = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
            MapPlayerObject mapPlayer = slinService.CreatePhotonMapPlayer(PhotonNetwork.LocalPlayer, _playground.GetStartRoundPos(indexPlayer));

            mapPlayer.Initizlize(indexPlayer);

            return mapPlayer;
        }

        public void PlayerReady() {
            Hashtable props = new Hashtable { { PhotonManagerConst.PLAYER_STAY_GAME, true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
            if (changedProps.ContainsKey(PhotonManagerConst.PLAYER_STAY_GAME)) CheckAllReady();
        }

        private void CheckAllReady() {

            var players = PhotonNetwork.PlayerList;
            
            int readyCount = 0;
            foreach (var p in players) {
                if (p.CustomProperties.TryGetValue(PhotonManagerConst.PLAYER_STAY_GAME, out var v) && (bool)v)
                    readyCount++;
            }

            if (readyCount == PhotonNetwork.CurrentRoom.PlayerCount) {
                GameRoot.ScenesManager.HideLoadingScreen();
                _mapManager.StartGame();
            }
        }

    }
}
