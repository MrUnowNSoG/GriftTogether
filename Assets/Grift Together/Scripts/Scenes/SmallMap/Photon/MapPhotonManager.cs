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
        
        public void Init(ServiceLocator serviceLocator, PlaygroundManager playground, MapManager mapManager) {
            _serviceLocator = serviceLocator;
            
            _playground = playground;
            _mapManager = mapManager;
            
            CreateLocalPlayer();
        }

        private void CreateLocalPlayer() {
            GameRoot.ServiceLocator.Resolve(out SkinPhotonService servic);
            int indexPlayer = Array.IndexOf(PhotonNetwork.PlayerList, PhotonNetwork.LocalPlayer);
            servic.CreatePhotonMapPlayer(PhotonNetwork.LocalPlayer, _playground.GetStartRoundPos(indexPlayer));
            
            MapPhotonRPCService rpcService = PhotonNetwork.Instantiate(PhotonPrefabConst.MAP_RPC_AGENT_PREFAB_PATH, Vector3.zero, Quaternion.identity).GetComponent<MapPhotonRPCService>();
            _serviceLocator.AddService(rpcService);
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
                _mapManager.StartSession();

                if (PhotonNetwork.IsMasterClient) {
                   _serviceLocator.Resolve(out MapPhotonRPCService service);
                    service.RPC_SendNextTurn();
                }
            }
        }

        public override void OnLeftRoom() {
            GameRoot.ScenesManager.ShowLoadingScreen();
            GameRoot.ScenesManager.SwitchScene(ScenesManagerConst.MENU_SCENE, true);
        }

    }
}
