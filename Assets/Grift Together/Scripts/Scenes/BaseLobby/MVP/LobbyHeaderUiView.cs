using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;

namespace GriftTogether {
    public class LobbyHeaderUiView : MonoBehaviourPunCallbacks, IView {

        [Space(0)][Header("Room stats")]
        [SerializeField] private TextMeshProUGUI _codeRoomText;
        [SerializeField] private TextMeshProUGUI _userInRoomText;

        [Space(10)][Header("Player Stats")]
        [SerializeField] private TextMeshProUGUI _countWinText;
        [SerializeField] private TextMeshProUGUI _countCoinText;

        public event Action OnClose;

        public void Initialize(IPresenter presenter) {
            _codeRoomText.text = GameRoot.PhotonManager.GetCodeRoom;
            UpdateUserCount();
        }

        public void UpdateData(int countWin, int countCoin) {
            _countWinText.text = countWin.ToString();
            _countCoinText.text = countCoin.ToString();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) => UpdateUserCount();
        public override void OnPlayerLeftRoom(Player player) => UpdateUserCount();

        private void UpdateUserCount() {
            var room = PhotonNetwork.CurrentRoom;

            if (room != null) {
                _userInRoomText.text = $"{room.PlayerCount}/{room.MaxPlayers}";
            } else {
                _userInRoomText.text = $"0/{GameRoot.PhotonManager.GetMaxPlayer}";
            }

        }

        public void ShowUI() { }
        public void CloseUI() { }
        public void Deinitialize() { }
    }
}
