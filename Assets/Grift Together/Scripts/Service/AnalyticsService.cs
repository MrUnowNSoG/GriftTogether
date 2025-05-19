using Photon.Pun;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GriftTogether {

    public class AnalyticsService : IService {

        private FireStoreManager _fireStore;

        public AnalyticsService(FireStoreManager firestore) {}

        public void UpdateAnalytics() {

        }

        public Task SentBugReportAsync(string title, string description) {
            Task task = Task.Run(() => SentBugRepost(title, description));
            return task;
        }

        public async void SentBugRepost(string title, string description) {
            BugReportFireStoreDTO data;
            data = new BugReportFireStoreDTO();
            data.UserName = PhotonNetwork.LocalPlayer.NickName;
            data.DataTime = DateTime.UtcNow.ToString();
            data.CountPlayer = PhotonNetwork.PlayerList.Length;

            data.TitleBug = title;
            data.DescriptionBug = description;

            string nameFile = title + " : " + DateTime.UtcNow.ToString();

            await _fireStore.SaveToCloud(data, FireStoreConst.BUG_COLLECTION, nameFile);
        }

        public Task SentPingPlayerAsync() {
            Task task = Task.Run(SentPingPlayer);
            return task;
        }

        public async void SentPingPlayer() {
            PingPlayerFireStoreDTO data = new PingPlayerFireStoreDTO();
            data.UserName = PhotonNetwork.LocalPlayer.NickName;
            data.DataTime = DateTime.UtcNow.ToString();
            data.CountPlayer = PhotonNetwork.PlayerList.Length;
            data.Ping = PhotonNetwork.GetPing();

            string nameFile = data.UserName + " : " + DateTime.UtcNow.ToString();
            await _fireStore.SaveToCloud(data, FireStoreConst.PING_COLLECTION, nameFile);
        }
    }
}
