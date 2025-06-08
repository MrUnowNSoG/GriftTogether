using Photon.Pun;
using System;

namespace GriftTogether {

    public class AnalyticsService : IService {

        private float DELEY_PING = 5;
        private float COUNT_PLAYER_FOR_PING = 3;

        private FireStoreManager _fireStore;

        private DateTime _oldTime;

        public AnalyticsService(FireStoreManager firestore) {
            _fireStore = firestore;
            _oldTime = DateTime.UtcNow;
        }

        public void UpdateAnalytics() {

            TimeSpan deley = DateTime.UtcNow.Subtract(_oldTime);

            if(deley.TotalMinutes >= DELEY_PING && PhotonNetwork.PlayerList.Length >= COUNT_PLAYER_FOR_PING) {
                SendPingPlayer();
                _oldTime = DateTime.UtcNow;
            } 
             
        }


        public async void SendBugRepost(string title, string description) {
            BugReportFireStoreDTO data;
            data = new BugReportFireStoreDTO();
            data.UserName = PhotonNetwork.LocalPlayer.NickName;
            data.DataTime = DateTime.UtcNow.ToString();
            data.CountPlayer = PhotonNetwork.PlayerList.Length;

            data.TitleBug = title;
            data.DescriptionBug = description;

            string nameFile = title + "-" + UnityEngine.Random.Range(0, 1_000_000); ;

            await _fireStore.SaveToCloud(data, FireStoreConst.BUG_COLLECTION, nameFile);
        }

        public async void SendPingPlayer() {
            PingPlayerFireStoreDTO data = new PingPlayerFireStoreDTO();
            data.UserName = PhotonNetwork.LocalPlayer.NickName;
            data.DataTime = DateTime.UtcNow.ToString();
            data.CountPlayer = PhotonNetwork.PlayerList.Length;
            data.Ping = PhotonNetwork.GetPing();

            string nameFile = data.UserName + "-" + UnityEngine.Random.Range(0, 1_000_000);
            await _fireStore.SaveToCloud(data, FireStoreConst.PING_COLLECTION, nameFile);
        }

        public async void SendBuildStat(string indeficator) {
            
            bool res = await _fireStore.ExistFile(FireStoreConst.BUILD_COLLECTION, indeficator, true);
            
            if (res == false) {
                BuildStatsFireStoreDTO data_local = new BuildStatsFireStoreDTO();
                data_local.Induficator = indeficator;
                data_local.LastActivate = DateTime.UtcNow.ToString();
                data_local.CountActivate = 1;

                await _fireStore.SaveToCloud(data_local, FireStoreConst.BUILD_COLLECTION, indeficator, true);
                return;
            }

            BuildStatsFireStoreDTO data_old = await _fireStore.TryGetFile<BuildStatsFireStoreDTO>(FireStoreConst.BUILD_COLLECTION, indeficator, true);

            BuildStatsFireStoreDTO data_new = new BuildStatsFireStoreDTO();
            data_new.Induficator = indeficator;
            data_new.LastActivate = DateTime.UtcNow.ToString();
            data_new.CountActivate = data_old.CountActivate + 1;


            await _fireStore.SaveToCloud(data_new, FireStoreConst.BUILD_COLLECTION, indeficator, true);
        }
    }
}
